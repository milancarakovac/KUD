using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Uplata
    {
        public int IdUplata { get; set; }
        public DateTime DatumUplate { get; set; }
        public double IznosUplate { get; set; }
        public int IdOsoba { get; set; }
        public int IdClan { get; set; }

        public Uplata(int idUplata, DateTime datumUplate, double iznosUplate, int idOsoba, int idClan)
        {
            IdUplata = idUplata;
            DatumUplate = datumUplate;
            IznosUplate = iznosUplate;
            IdOsoba = idOsoba;
            IdClan = idClan;
        }

        public override bool Equals(object obj)
        {
            return obj is Uplata uplata &&
                   IdUplata == uplata.IdUplata &&
                   DatumUplate == uplata.DatumUplate &&
                   IznosUplate == uplata.IznosUplate &&
                   IdOsoba == uplata.IdOsoba &&
                   IdClan == uplata.IdClan;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdUplata, DatumUplate, IznosUplate, IdOsoba, IdClan);
        }
    }
}
