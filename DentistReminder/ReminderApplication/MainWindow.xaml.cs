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
            DataContext = new MainViewModel();
        }
        
        
    }
}
