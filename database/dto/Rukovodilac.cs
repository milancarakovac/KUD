using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Rukovodilac : Osoba
    {
        public string Pozicija { get; set; }
        public Rukovodilac(int idOsoba, string ime, string prezime, string jmbg, string brojTelefona, string email, DateTime datumRodjenja, string pozicija) : base(idOsoba, ime, prezime, jmbg, brojTelefona, email, datumRodjenja)
        {
            Pozicija = pozicija;
        }

        public override bool Equals(object obj)
        {
            return obj is Rukovodilac rukovodilac &&
                   base.Equals(obj) &&
                   IdOsoba == rukovodilac.IdOsoba &&
                   Ime == rukovodilac.Ime &&
                   Prezime == rukovodilac.Prezime &&
                   Jmbg == rukovodilac.Jmbg &&
                   BrojTelefona == rukovodilac.BrojTelefona &&
                   Email == rukovodilac.Email &&
                   DatumRodjenja == rukovodilac.DatumRodjenja &&
                   Pozicija == rukovodilac.Pozicija;
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
            hash.Add(Pozicija);
            return hash.ToHashCode();
        }
    }
}
