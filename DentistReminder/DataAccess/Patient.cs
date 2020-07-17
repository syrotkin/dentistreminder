using System;

namespace DataAccess
{
    public class Patient
    {
        public int Pid { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public DateTime? LastVisit { get; set; }

        public string PhoneNumber { get; set; }

        public string Treatment { get; set; }
    }
}
