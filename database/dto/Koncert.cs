using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Koncert
    {
        public int IdKoncert { get; set; }
        public string Naziv { get; set; }
        public string Mjesto { get; set; }
        public DateTime Datum { get; set; }

        public Koncert(int idKoncert, string naziv, string mjesto, DateTime datum)
        {
            IdKoncert = idKoncert;
            Naziv = naziv;
            Mjesto = mjesto;
            Datum = datum;
        }

        public override bool Equals(object obj)
        {
            return obj is Koncert koncert &&
                   IdKoncert == koncert.IdKoncert &&
                   Naziv == koncert.Naziv &&
                   Mjesto == koncert.Mjesto &&
                   Datum == koncert.Datum;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdKoncert, Naziv, Mjesto, Datum);
        }
    }
}
