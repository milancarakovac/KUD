using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class SponzorUplata
    {
        public int IdSponzorUplata { get; set; }
        public int IdSponzor { get; set; }
        public int IdUplata { get; set; }

        public SponzorUplata(int idSponzorUplata, int idSponzor, int idUplata)
        {
            IdSponzorUplata = idSponzorUplata;
            IdSponzor = idSponzor;
            IdUplata = idUplata;
        }

        public override bool Equals(object obj)
        {
            return obj is SponzorUplata uplata &&
                   IdSponzorUplata == uplata.IdSponzorUplata &&
                   IdSponzor == uplata.IdSponzor &&
                   IdUplata == uplata.IdUplata;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdSponzorUplata, IdSponzor, IdUplata);
        }
    }
}
