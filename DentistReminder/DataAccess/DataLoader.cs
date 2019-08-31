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
                    command.CommandText = @"select PID, LastName, date(LastVisit) as LastVisit
                                            from patient
                                            where julianday('now') - julianday(lastvisit) >= 183";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pid = Convert.ToInt32(reader["PID"]);
                            var name = Convert.ToString(reader["LastName"]);
                            var lastVisit = Convert.ToDateTime(reader["LastVisit"]);
                            var patient = new Patient
                            {
                                Pid = pid,
                                LastName = name,
                                LastVisit = lastVisit
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
