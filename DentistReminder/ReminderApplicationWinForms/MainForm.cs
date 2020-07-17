using System;
using System.Windows.Forms;

namespace ReminderApplicationWinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var cleaningDisplayer = new PatientDisplayer
            {
                DaysBeforeReminder = 183,
                Treatment = "Cleaning"
            };
            cleaningTab.Controls.Add(cleaningDisplayer);
            cleaningDisplayer.Dock = DockStyle.Fill;

            var orthodonticsDisplayer = new PatientDisplayer
            {
                DaysBeforeReminder = 30,
                Treatment = "Orthodontics"
            };
            orthodonticsTab.Controls.Add(orthodonticsDisplayer);
            orthodonticsDisplayer.Dock = DockStyle.Fill;
        }

        private void MainFormLoad(object sender, EventArgs e)
        {

        }
    }
}
