using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class ClanIzvodjackaGrupa
    {
        public int IdOsoba { get; set; }
        public int IdIzvodjackaGrupa { get; set; }

        public ClanIzvodjackaGrupa(int idOsoba, int idIzvodjackaGrupa)
        {
            IdOsoba = idOsoba;
            IdIzvodjackaGrupa = idIzvodjackaGrupa;
        }

        public override bool Equals(object obj)
        {
            return obj is ClanIzvodjackaGrupa grupa &&
                   IdOsoba == grupa.IdOsoba &&
                   IdIzvodjackaGrupa == grupa.IdIzvodjackaGrupa;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdOsoba, IdIzvodjackaGrupa);
        }
    }
}
