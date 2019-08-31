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
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var loader = new DataLoader(connectionString);
            loader.Load();
        }
    }
}
