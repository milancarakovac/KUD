using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Nastup
    {
        public int IdNastup { get; set; }
        public DateTime Datum { get; set; }
        public string Mjesto { get; set; }
        public string Naziv { get; set; }

        public Nastup(int idNastup, DateTime datum, string mjesto, string naziv)
        {
            IdNastup = idNastup;
            Datum = datum;
            Mjesto = mjesto;
            Naziv = naziv;
        }

        public override bool Equals(object obj)
        {
            return obj is Nastup nastup &&
                   IdNastup == nastup.IdNastup &&
                   Datum == nastup.Datum &&
                   Mjesto == nastup.Mjesto &&
                   Naziv == nastup.Naziv;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdNastup, Datum, Mjesto, Naziv);
        }
    }
}
