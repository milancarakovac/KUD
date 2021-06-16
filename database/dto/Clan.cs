using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Clan : Osoba
    {
        public string UzrasnaGrupa { get; set; }
        public bool Obrisan { get; set; }
        public Clan(int idOsoba, string ime, string prezime, string jmbg, string brojTelefona, string email, DateTime datumRodjenja, string uzrasnaGrupa, bool obrisan) : base(idOsoba, ime, prezime, jmbg, brojTelefona, email, datumRodjenja)
        {
            UzrasnaGrupa = uzrasnaGrupa;
            Obrisan = obrisan;
        }

        public override bool Equals(object obj)
        {
            return obj is Clan clan &&
                   base.Equals(obj) &&
                   IdOsoba == clan.IdOsoba &&
                   Ime == clan.Ime &&
                   Prezime == clan.Prezime &&
                   Jmbg == clan.Jmbg &&
                   BrojTelefona == clan.BrojTelefona &&
                   Email == clan.Email &&
                   DatumRodjenja == clan.DatumRodjenja &&
                   UzrasnaGrupa == clan.UzrasnaGrupa;
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
            hash.Add(UzrasnaGrupa);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return Ime + " (" + Jmbg + ") " + Prezime;
        }
    }
}
