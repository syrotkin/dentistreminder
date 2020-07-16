using DataAccess;
using System;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace ReminderApplicationWinForms
{
    public partial class MainForm : Form
    {
        private readonly DataLoader dataLoader;

        public MainForm()
        {
            InitializeComponent();

            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            dataLoader = new DataLoader(connectionString);
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            patientGrid.DataSource = mainBindingSource;
            LoadAllPatients();
        }

        private void LoadAllPatientsWithDataAdapter()
        {
            mainBindingSource.DataSource = dataLoader.LoadWithDataAdapter();
        }
        
        private void LoadOverduePatients()
        {
            var patients = dataLoader.LoadOverduePatients();
            mainBindingSource.DataSource = patients;

            ConfigureColumns();
        }

        private void LoadAllPatients()
        {
            var patients = dataLoader.LoadAllPatients();
            mainBindingSource.DataSource = patients;

            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            var pidColumn = patientGrid.Columns["PID"];
            if (pidColumn != null)
            {
                pidColumn.Visible = false;
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

        private void PatientGridRowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!patientGrid.IsCurrentRowDirty)
            {
                return;
            }

            if (!(mainBindingSource.DataSource is List<Patient> patients))
            {
                return;
            }

            var row = patientGrid.Rows[e.RowIndex];
            int.TryParse(row.Cells["PID"].Value.ToString(), out int pid);

            if (pid == 0 || FirstOrDefault(patients, pid) == null)
            {
                var patient = new Patient
                {
                    LastName = row.Cells["LastName"]?.Value.ToString(),
                    FirstName = row.Cells["FirstName"]?.Value.ToString(),
                    Patronymic = row.Cells["Patronymic"]?.Value.ToString(),
                    LastVisit = ToDate(row.Cells["LastVisit"]?.Value.ToString()),
                    PhoneNumber = row.Cells["PhoneNumber"]?.Value.ToString()
                };
                dataLoader.Insert(patient);
            }
            else
            {
                var updatedPatient = FirstOrDefault(patients, pid);
                if (updatedPatient != null)
                {
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

            if (DateTime.TryParse(dateInput, new DateTimeFormatInfo(), DateTimeStyles.AssumeLocal, out DateTime result))
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
    }
}
