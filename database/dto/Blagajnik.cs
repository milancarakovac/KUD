using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Blagajnik : Osoba
    {
        public Blagajnik(int idOsoba, string ime, string prezime, string jmbg, string brojTelefona, string email, DateTime datumRodjenja) : base(idOsoba, ime, prezime, jmbg, brojTelefona, email, datumRodjenja)
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Blagajnik blagajnik &&
                   base.Equals(obj) &&
                   IdOsoba == blagajnik.IdOsoba &&
                   Ime == blagajnik.Ime &&
                   Prezime == blagajnik.Prezime &&
                   Jmbg == blagajnik.Jmbg &&
                   BrojTelefona == blagajnik.BrojTelefona &&
                   Email == blagajnik.Email &&
                   DatumRodjenja == blagajnik.DatumRodjenja;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), IdOsoba, Ime, Prezime, Jmbg, BrojTelefona, Email, DatumRodjenja);
        }
    }
}
