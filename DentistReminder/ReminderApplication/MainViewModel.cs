using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;

using ReminderApplication.Annotations;

namespace ReminderApplication {
    public class  MainViewModel : INotifyPropertyChanged {

        private const string CONNECTION_STRING = "Data Source=dentist.db;Pooling=true;";
        
        public MainViewModel() {
            m_patients = new List<Patient>();
            LoadPatients();    
        }
        
        private void LoadPatients() {
            using (IDbConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand()) {
                    command.CommandText = @"select PID, LastName, date(LastVisit) as LastVisit
                                            from patient
                                            where julianday('now') - julianday(lastvisit) >= 183";
                    using (IDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var rid = Convert.ToInt32(reader["PID"]);
                            var name = Convert.ToString(reader["LastName"]);
                            var lastVisit = Convert.ToDateTime(reader["LastVisit"]);
                            var patient = new Patient {
                                Pid = rid,
                                LastName = name,
                                LastVisit = lastVisit
                            };
                            m_patients.Add(patient);
                        }
                    }
                }
                connection.Close();
            }
        }
        
        private IList<Patient> m_patients;
        public IList<Patient> Patients {
            get => m_patients;
            set {
                m_patients = value;
                OnPropertyChanged(nameof(Patients));
            }
        }

        private void InsertIntoDatabase() {
            using (IDbConnection connection = new SQLiteConnection(CONNECTION_STRING)) {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand()) {
                    command.CommandText = "insert into Patient (PID, LastName, FirstName, Patronymic, LastVisit, PhoneNumber) values (1, 'Петренко', 'Иван', 'Иванович', '2015-02-23', '223-24-25')";
                    var countUpdated = command.ExecuteNonQuery();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
