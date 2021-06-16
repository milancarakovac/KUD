using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUD.Tables
{
    class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
        public int IdPerson { get; set; }

        public Member(string firstName, string lastName, string group, int idPerson)
        {
            FirstName = firstName;
            LastName = lastName;
            Group = group;
            IdPerson = idPerson;
        }
    }
}
