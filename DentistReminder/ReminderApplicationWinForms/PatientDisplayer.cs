using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using DataAccess;

namespace ReminderApplicationWinForms
{
    public partial class PatientDisplayer : UserControl
    {
        private DataLoader dataLoader;

        public int DaysBeforeReminder { get; set; } = 183;
        public string Treatment { get; set; }

        public PatientDisplayer()
        {
            InitializeComponent();
        }

        private void PatientDisplayer_Load(object sender, EventArgs e)
        {
            // TODO: check if has been defined already

            dataLoader = new DataLoader();

            patientGrid.DataSource = patientBindingSource;
            LoadAllPatients();
        }

        private void LoadAllPatients()
        {
            var patients = dataLoader.LoadAllPatients(Treatment);
            patientBindingSource.DataSource = patients;

            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            var pidColumn = patientGrid.Columns["PID"];
            if (pidColumn != null)
            {
                pidColumn.Visible = false;
            }

            var treatmentColumn = patientGrid.Columns["Treatment"];
            if (treatmentColumn != null)
            {
                treatmentColumn.Visible = false;
            }

            SetColumnHeaders();

            SetColumnWidths();
        }

        private void SetColumnHeaders()
        {
            var headers = new Dictionary<string, string>
            {
                [nameof(Patient.LastName)] = "Фамилия",
                [nameof(Patient.FirstName)] = "Имя",
                [nameof(Patient.Patronymic)] = "Oтчество",
                [nameof(Patient.LastVisit)] = "Дата посещения",
                [nameof(Patient.PhoneNumber)] = "Номер телефона"
            };

            foreach (var header in headers)
            {
                SetHeader(header.Key, header.Value);
            }
        }

        private void SetColumnWidths()
        {
            foreach (DataGridViewColumn dataGridViewColumn in patientGrid.Columns)
            {
                dataGridViewColumn.Width = 200;
            }
        }

        private void SetHeader(string propertyName, string header)
        {
            var column = patientGrid.Columns[propertyName];
            if (column != null)
            {
                column.HeaderText = header;
            }
        }

        private void patientGrid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!patientGrid.IsCurrentRowDirty)
            {
                return;
            }

            if (!(patientBindingSource.DataSource is List<Patient> patients))
            {
                return;
            }

            var row = patientGrid.Rows[e.RowIndex];
            int.TryParse(row.Cells["PID"].Value.ToString(), out int pid);

            if (pid == 0 || FirstOrDefault(patients, pid) == null)
            {
                // TODO: validation: check which fields are not null, allow null fields to be inserted
                var patient = new Patient
                {
                    LastName = row.Cells["LastName"]?.Value?.ToString(),
                    FirstName = row.Cells["FirstName"]?.Value?.ToString(),
                    Patronymic = row.Cells["Patronymic"]?.Value?.ToString(),
                    LastVisit = ToDate(row.Cells["LastVisit"]?.Value?.ToString()),
                    PhoneNumber = row.Cells["PhoneNumber"]?.Value?.ToString(),
                    Treatment = Treatment
                };
                dataLoader.Insert(patient);
            }
            else
            {
                var updatedPatient = FirstOrDefault(patients, pid);
                if (updatedPatient != null)
                {
                    updatedPatient.Treatment = Treatment;
                    dataLoader.Update(updatedPatient);
                }
            }
        }

        private static DateTime? ToDate(string dateInput)
        {
            if (string.IsNullOrEmpty(dateInput))
            {
                return null;
            }

            if (DateTime.TryParse(dateInput, CultureInfo.CreateSpecificCulture("ru-UA"), DateTimeStyles.AssumeLocal, out DateTime result))
            {
                return result;
            }

            return null;
        } 
    
        private static Patient FirstOrDefault(IEnumerable<Patient> patients, int pid)
        {
            foreach (var patient in patients)
            {
                if (patient.Pid == pid)
                {
                    return patient;
                }
            }

            return null;
        }

        private void CheckBoxOverdueCheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            if (checkbox.Checked)
            {
                LoadOverduePatients();
            }
            else
            {
                LoadAllPatients();
            }
        }

        private void LoadOverduePatients()
        {
            var patients = dataLoader.LoadOverduePatients(Treatment, DaysBeforeReminder);
            patientBindingSource.DataSource = patients;

            ConfigureColumns();
        }
    }
}
