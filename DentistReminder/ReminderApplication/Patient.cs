using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using ReminderApplication.Annotations;

namespace ReminderApplication {
    public class Patient : INotifyPropertyChanged  {

        private int m_pid;
        private string m_lastName;
        private DateTime m_lastVisit;

        public int Pid {
            get {
                return m_pid;
            }
            set {
                m_pid = value;
                OnPropertyChanged("Pid");
            }
        }

        public string LastName {
            get {
                return m_lastName;
            }
            set {
                m_lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public DateTime LastVisit {
            get {
                return m_lastVisit;
            }
            set {
                m_lastVisit = value;
                OnPropertyChanged("LastVisit");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
