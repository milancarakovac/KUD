using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUD.Tables
{
    class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public bool Administrator { get; set; }
        public int IdPerson { get; set; }
        public int IdAccount { get; set; }

        public Account(string firstName, string lastName, string username, bool administrator, int idPerson, int idAccount)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Administrator = administrator;
            IdPerson = idPerson;
            IdAccount = idAccount;
        }
    }
}
