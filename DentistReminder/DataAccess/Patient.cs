using System;

namespace DataAccess
{
    public class Patient {
        public int Pid {
            get; set;
        }

        public string LastName {
            get; set;
        }

        public DateTime LastVisit {
            get; set;
        }
    }
}
