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

            var myobraceDisplayer = new PatientDisplayer
            {
                DaysBeforeReminder = 30,
                Treatment = "Myobrace"
            };
            myobraceTab.Controls.Add(myobraceDisplayer);
            myobraceDisplayer.Dock = DockStyle.Fill;
        }

        private void MainFormLoad(object sender, EventArgs e)
        {

        }
    }
}
