using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class KoncertNastup
    {
        public int IdNastup { get; set; }
        public int IdKoncert { get; set; }

        public KoncertNastup(int idNastup, int idKoncert)
        {
            IdNastup = idNastup;
            IdKoncert = idKoncert;
        }

        public override bool Equals(object obj)
        {
            return obj is KoncertNastup nastup &&
                   IdNastup == nastup.IdNastup &&
                   IdKoncert == nastup.IdKoncert;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdNastup, IdKoncert);
        }
    }
}
