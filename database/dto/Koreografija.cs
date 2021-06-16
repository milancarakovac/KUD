using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Koreografija
    {
        public int IdKoreografija { get; set; }
        public string Vlasnik { get; set; }
        public DateTime VrijediDo { get; set; }
        public double Cijena { get; set; }
        public int IdOsoba { get; set; }

        public Koreografija(int idKoreografija, string vlasnik, DateTime vrijediDo, double cijena, int idOsoba)
        {
            IdKoreografija = idKoreografija;
            Vlasnik = vlasnik;
            VrijediDo = vrijediDo;
            Cijena = cijena;
            IdOsoba = idOsoba;
        }

        public override bool Equals(object obj)
        {
            return obj is Koreografija koreografija &&
                   IdKoreografija == koreografija.IdKoreografija &&
                   Vlasnik == koreografija.Vlasnik &&
                   VrijediDo == koreografija.VrijediDo &&
                   Cijena == koreografija.Cijena &&
                   IdOsoba == koreografija.IdOsoba;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdKoreografija, Vlasnik, VrijediDo, Cijena, IdOsoba);
        }
    }
}
