using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Trener : Osoba
    {
        public string BrojLicence { get; set; }
        public Trener(int idOsoba, string ime, string prezime, string jmbg, string brojTelefona, string email, DateTime datumRodjenja, string brojLicence) : base(idOsoba, ime, prezime, jmbg, brojTelefona, email, datumRodjenja)
        {
            BrojLicence = brojLicence;
        }

        public override bool Equals(object obj)
        {
            return obj is Trener trener &&
                   base.Equals(obj) &&
                   IdOsoba == trener.IdOsoba &&
                   Ime == trener.Ime &&
                   Prezime == trener.Prezime &&
                   Jmbg == trener.Jmbg &&
                   BrojTelefona == trener.BrojTelefona &&
                   Email == trener.Email &&
                   DatumRodjenja == trener.DatumRodjenja &&
                   BrojLicence == trener.BrojLicence;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(IdOsoba);
            hash.Add(Ime);
            hash.Add(Prezime);
            hash.Add(Jmbg);
            hash.Add(BrojTelefona);
            hash.Add(Email);
            hash.Add(DatumRodjenja);
            hash.Add(BrojLicence);
            return hash.ToHashCode();
        }
    }
}
