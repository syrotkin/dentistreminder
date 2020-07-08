using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;

namespace DataAccess
{
    public class DataLoader
    {
        private readonly string connectionString;
        private SQLiteDataAdapter dataAdapter;
        private SQLiteCommandBuilder commandBuilder;

        private const string SelectCommand = @"select PID, LastName, FirstName, Patronymic, PhoneNumber, date(LastVisit) as LastVisit
                                            from patient
                                            where julianday('now') - julianday(lastvisit) >= 183";

        public DataLoader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Patient> Load()
        {
           return LoadPatients();
        }

        public DataTable LoadWithDataAdapter()
        {
            dataAdapter = new SQLiteDataAdapter(SelectCommand, connectionString);
            
            commandBuilder = new SQLiteCommandBuilder(dataAdapter);
            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            
            var dataTable = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        private List<Patient> LoadPatients()
        {
            var patients = new List<Patient>();
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = SelectCommand;
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

        public void UpdateFromDataTable(object dataSource)
        {
            var dataTable = dataSource as DataTable;
            if (dataTable == null)
            {
                return;
            }

            dataAdapter.Update(dataTable);
        }

        public void Update(Patient patient)
        {
            const string updateCommand = @"UPDATE [Patient]
                        SET
                        [LastName] = @lastName,
                        [FirstName] = @firstName,
                        [Patronymic] = @patronymic,
                        [LastVisit] = @lastVisit,
                        [PhoneNumber] = @phoneNumber,
                        WHERE [PID] = @pid";

            ExecuteCommand(patient, updateCommand);
        }

        private void ExecuteCommand(Patient patient, string commandText)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;

                    AddParameters(patient, command);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private static void AddParameters(Patient patient, IDbCommand command)
        {
            var parameters = new Dictionary<string, object>
            {
                ["@pid"] = patient.Pid,
                ["@lastName"] = patient.LastName,
                ["@firstName"] = patient.FirstName,
                ["@patronymic"] = patient.Patronymic,
                ["@lastVisit"] = patient.LastVisit,
                ["@phoneNumber"] = patient.PhoneNumber
            };

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(new SQLiteParameter {ParameterName = parameter.Key, Value = parameter.Value});
            }
        }

        public void Insert(Patient patient)
        {
            const string insertCommand = @"INSERT INTO [Patient]
                                (LastName, FirstName, Patronymic, LastVisit, PhoneNumber)
                                VALUES
                                (@lastName, @firstName, @patronymic, @lastVisit, @phoneNumber)";

            ExecuteCommand(patient, insertCommand);
        }
    }
}
