using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Donacija
    {
        public int IdDonacija { get; set; }
        public int IdOsoba { get; set; }
        public string TipDonacije { get; set; }
        public int IdUplata { get; set; }

        public Donacija(int idDonacija, int idOsoba, string tipDonacije, int idUplata)
        {
            IdDonacija = idDonacija;
            IdOsoba = idOsoba;
            TipDonacije = tipDonacije;
            IdUplata = idUplata;
        }

        public override bool Equals(object obj)
        {
            return obj is Donacija donacija &&
                   IdDonacija == donacija.IdDonacija &&
                   IdOsoba == donacija.IdOsoba &&
                   TipDonacije == donacija.TipDonacije &&
                   IdUplata == donacija.IdUplata;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdDonacija, IdOsoba, TipDonacije, IdUplata);
        }
    }
}
