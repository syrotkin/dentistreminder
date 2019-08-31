using DataAccess;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace ReminderApplicationWinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            LoadData();
        }
         
        private void LoadData()
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var loader = new DataLoader(connectionString);
            var patients = loader.Load();

            foreach (var patient in patients)
            {
                patientGrid.Rows.Add(patient.Pid, patient.LastName, patient.FirstName, patient.Patronymic, patient.PhoneNumber, patient.LastVisit);
            }
        }
    }
}
