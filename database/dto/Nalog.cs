using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Nalog
    {
        public int IdNalog { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public bool Administrator { get; set; }
        public int IdOsoba { get; set; }
        public string Jezik { get; set; }
        public string Tema { get; set; }

        public Nalog(int idNalog, string korisnickoIme, string lozinka, bool administrator, int idOsoba)
        {
            IdNalog = idNalog;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Administrator = administrator;
            IdOsoba = idOsoba;
            Jezik = "sr-Latn";
            Tema = "crna";
        }

        public override bool Equals(object obj)
        {
            return obj is Nalog nalog &&
                   IdNalog == nalog.IdNalog &&
                   KorisnickoIme == nalog.KorisnickoIme &&
                   Lozinka == nalog.Lozinka &&
                   Administrator == nalog.Administrator &&
                   IdOsoba == nalog.IdOsoba;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdNalog, KorisnickoIme, Lozinka, Administrator, IdOsoba);
        }
    }
}
