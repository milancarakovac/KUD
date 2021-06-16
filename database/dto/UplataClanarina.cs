using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class UplataClanarina
    {
        public int IdOsoba { get; set; }
        public int IdUplata { get; set; }

        public UplataClanarina(int idOsoba, int idUplata)
        {
            IdOsoba = idOsoba;
            IdUplata = idUplata;
        }

        public override bool Equals(object obj)
        {
            return obj is UplataClanarina clanarina &&
                   IdOsoba == clanarina.IdOsoba &&
                   IdUplata == clanarina.IdUplata;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdOsoba, IdUplata);
        }
    }
}
