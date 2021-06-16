using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class PrijateljKUDa : Osoba
    {
        public PrijateljKUDa(int idOsoba, string ime, string prezime, string jmbg, string brojTelefona, string email, DateTime datumRodjenja) : base(idOsoba, ime, prezime, jmbg, brojTelefona, email, datumRodjenja)
        {

        }

        public override bool Equals(object obj)
        {
            return obj is PrijateljKUDa da &&
                   base.Equals(obj) &&
                   IdOsoba == da.IdOsoba &&
                   Ime == da.Ime &&
                   Prezime == da.Prezime &&
                   Jmbg == da.Jmbg &&
                   BrojTelefona == da.BrojTelefona &&
                   Email == da.Email &&
                   DatumRodjenja == da.DatumRodjenja;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), IdOsoba, Ime, Prezime, Jmbg, BrojTelefona, Email, DatumRodjenja);
        }
    }
}
