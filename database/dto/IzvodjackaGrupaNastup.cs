using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class IzvodjackaGrupaNastup
    {
        public int IdIzvodjackaGrupa { get; set; }
        public int IdNastup { get; set; }

        public IzvodjackaGrupaNastup(int idIzvodjackaGrupa, int idNastup)
        {
            IdIzvodjackaGrupa = idIzvodjackaGrupa;
            IdNastup = idNastup;
        }

        public override bool Equals(object obj)
        {
            return obj is IzvodjackaGrupaNastup nastup &&
                   IdIzvodjackaGrupa == nastup.IdIzvodjackaGrupa &&
                   IdNastup == nastup.IdNastup;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdIzvodjackaGrupa, IdNastup);
        }
    }
}
