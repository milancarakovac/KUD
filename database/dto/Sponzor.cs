using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Sponzor
    {
        public int IdSponzor { get; set; }
        public string Naziv { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }

        public Sponzor(int idSponzor, string naziv, string brojTelefona, string email)
        {
            IdSponzor = idSponzor;
            Naziv = naziv;
            BrojTelefona = brojTelefona;
            Email = email;
        }

        public override bool Equals(object obj)
        {
            return obj is Sponzor sponzor &&
                   IdSponzor == sponzor.IdSponzor &&
                   Naziv == sponzor.Naziv &&
                   BrojTelefona == sponzor.BrojTelefona &&
                   Email == sponzor.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdSponzor, Naziv, BrojTelefona, Email);
        }
    }
}
