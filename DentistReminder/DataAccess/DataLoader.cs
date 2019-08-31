using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace DataAccess
{
    public class DataLoader
    {
        private string connectionString;

        public DataLoader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Patient> Load()
        {
           return LoadPatients();
        }

        private List<Patient> LoadPatients()
        {
            var patients = new List<Patient>();
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select PID, LastName, FirstName, Patronymic, PhoneNumber, date(LastVisit) as LastVisit
                                            from patient
                                            where julianday('now') - julianday(lastvisit) >= 183";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var patient = new Patient
                            {
                                Pid = Convert.ToInt32(reader["PID"]),
                                LastName = Convert.ToString(reader["LastName"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                Patronymic = Convert.ToString(reader["Patronymic"]),
                                PhoneNumber = Convert.ToString(reader["PhoneNumber"]),
                                LastVisit = Convert.ToDateTime(reader["LastVisit"])
                            };
                            patients.Add(patient);
                        }
                    }
                }
                connection.Close();
            }
            return patients;
        }
    }
}
