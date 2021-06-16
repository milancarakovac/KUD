using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Osoba
    {
        public int IdOsoba { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
        public DateTime DatumRodjenja { get; set; }

        public Osoba(int idOsoba, string ime, string prezime, string jmbg, string brojTelefona, string email, DateTime datumRodjenja)
        {
            IdOsoba = idOsoba;
            Ime = ime;
            Prezime = prezime;
            Jmbg = jmbg;
            BrojTelefona = brojTelefona;
            Email = email;
            DatumRodjenja = datumRodjenja;
        }

        public override bool Equals(object obj)
        {
            return obj is Osoba osoba &&
                   IdOsoba == osoba.IdOsoba &&
                   Ime == osoba.Ime &&
                   Prezime == osoba.Prezime &&
                   Jmbg == osoba.Jmbg &&
                   BrojTelefona == osoba.BrojTelefona &&
                   Email == osoba.Email &&
                   DatumRodjenja == osoba.DatumRodjenja;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdOsoba, Ime, Prezime, Jmbg, BrojTelefona, Email, DatumRodjenja);
        }
    }
}
