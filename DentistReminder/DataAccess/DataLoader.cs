using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Configuration;

namespace DataAccess
{
    public class DataLoader
    {
        private readonly string connectionString;
        private SQLiteDataAdapter dataAdapter;
        private SQLiteCommandBuilder commandBuilder;

        public DataLoader()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
      
        private const string SelectOverduePatientsCommand = @"select PID, LastName, FirstName, Patronymic, PhoneNumber, date(LastVisit) as LastVisit
                                            from Patient
                                            where Treatment = @treatment and julianday('now') - julianday(LastVisit) >= @days";

        private const string SelectAllPatientsCommand = @"select PID, LastName, FirstName, Patronymic, PhoneNumber, date(LastVisit) as LastVisit
                                            from Patient
                                            where Treatment = @treatment";

        public DataTable LoadWithDataAdapter()
        {
            dataAdapter = new SQLiteDataAdapter(SelectAllPatientsCommand, connectionString);
            
            commandBuilder = new SQLiteCommandBuilder(dataAdapter);
            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            
            var dataTable = new DataTable
            {
                Locale = CultureInfo.InvariantCulture
            };
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public List<Patient> LoadAllPatients(string treatment)
        {
            return LoadPatients(SelectAllPatientsCommand, treatment);
        }

        public List<Patient> LoadOverduePatients(string treatment, int daysBeforeReminder)
        {
            return LoadPatients(SelectOverduePatientsCommand, treatment, daysBeforeReminder);
        }

        private int GetNextPid()
        {
            const string commandText = @"select seq from sqlite_sequence
            where name = 'Patient'";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private List<Patient> LoadPatients(string commandText, string treatment, int days = 0)
        {
            var patients = new List<Patient>();
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.Parameters.Add(new SQLiteParameter
                    {
                        ParameterName = "@days",
                        Value = days
                    });
                    command.Parameters.Add(new SQLiteParameter
                    {
                        ParameterName = "@treatment",
                        Value = treatment
                    });
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var patient = new Patient
                            {
                                Pid = Convert.ToInt32(reader["PID"]),
                                LastName = TryConvertToString(reader["LastName"]),
                                FirstName = TryConvertToString(reader["FirstName"]),
                                Patronymic = TryConvertToString(reader["Patronymic"]),
                                PhoneNumber = TryConvertToString(reader["PhoneNumber"]),
                                LastVisit = TryConvertToDateTime(reader["LastVisit"])
                            };
                            patients.Add(patient);
                        }
                    }
                }
                connection.Close();
            }
            return patients;
        }

        private static DateTime? TryConvertToDateTime(object o)
        {
            return o is DBNull ? (DateTime?)null : Convert.ToDateTime(o);
        }

        private static string TryConvertToString(object o)
        {
            return o is DBNull ? null : Convert.ToString(o);
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
                        [Treatment] = @treatment
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
                ["@lastName"] = patient.LastName ?? "ERROR",
                ["@firstName"] = patient.FirstName,
                ["@patronymic"] = patient.Patronymic,
                ["@lastVisit"] = patient.LastVisit,
                ["@phoneNumber"] = patient.PhoneNumber,
                ["@treatment"] = patient.Treatment
            };

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(new SQLiteParameter {ParameterName = parameter.Key, Value = parameter.Value});
            }
        }

        public int Insert(Patient patient)
        {
            const string insertCommand = @"INSERT INTO [Patient]
                                (LastName, FirstName, Patronymic, LastVisit, PhoneNumber, Treatment)
                                VALUES
                                (@lastName, @firstName, @patronymic, @lastVisit, @phoneNumber, @treatment)";

            ExecuteCommand(patient, insertCommand);

            return GetNextPid();
        }
    }
}
