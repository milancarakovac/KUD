using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Gost
    {
        public int IdGost { get; set; }
        public string Ime { get; set; }
        public string Mjesto { get; set; }
        public int IdKoncert { get; set; }

        public Gost(int idGost, string ime, string mjesto, int idKoncert)
        {
            IdGost = idGost;
            Ime = ime;
            Mjesto = mjesto;
            IdKoncert = idKoncert;
        }

        public override bool Equals(object obj)
        {
            return obj is Gost gost &&
                   IdGost == gost.IdGost &&
                   Ime == gost.Ime &&
                   Mjesto == gost.Mjesto &&
                   IdKoncert == gost.IdKoncert;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdGost, Ime, Mjesto, IdKoncert);
        }
    }
}
