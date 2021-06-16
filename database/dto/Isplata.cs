using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Isplata
    {
        public int IdIsplata { get; set; }
        public double Iznos { get; set; }
        public string Primalac { get; set; }
        public DateTime DatumIsplate { get; set; }
        public string BrojRacuna { get; set; }
        public int IdOsoba { get; set; }

        public Isplata(int idIsplata, double iznos, string primalac, DateTime datumIsplate, string brojRacuna, int idOsoba)
        {
            IdIsplata = idIsplata;
            Iznos = iznos;
            Primalac = primalac;
            DatumIsplate = datumIsplate;
            BrojRacuna = brojRacuna;
            IdOsoba = idOsoba;
        }

        public override bool Equals(object obj)
        {
            return obj is Isplata isplata &&
                   IdIsplata == isplata.IdIsplata &&
                   Iznos == isplata.Iznos &&
                   Primalac == isplata.Primalac &&
                   DatumIsplate == isplata.DatumIsplate &&
                   BrojRacuna == isplata.BrojRacuna &&
                   IdOsoba == isplata.IdOsoba;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdIsplata, Iznos, Primalac, DatumIsplate, BrojRacuna, IdOsoba);
        }
    }
}
