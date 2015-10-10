using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace ReminderApplication {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender,
                RoutedEventArgs e) {
            ConnectToDatabase();
        }

        private static void ConnectToDatabase() {
            const string connectionString = "Data Source=dentist.db;Pooling=true;";
            using (IDbConnection connection = new SQLiteConnection(connectionString)) {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand()) {
                    command.CommandText = "Select RID, Name from test";
                    using (IDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            int fieldCount = reader.FieldCount;
                            var rid = Convert.ToInt32(reader["RID"]);
                            var name = Convert.ToString(reader["Name"]);
                        }
                    }
                }
                connection.Close();
            }
        }

    }
}
