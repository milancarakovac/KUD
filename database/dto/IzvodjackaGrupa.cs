using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class IzvodjackaGrupa
    {
        public int IdIzvodjackaGrupa { get; set; }
        public string Naziv { get; set; }

        public IzvodjackaGrupa(int idIzvodjackaGrupa, string naziv)
        {
            IdIzvodjackaGrupa = idIzvodjackaGrupa;
            Naziv = naziv;
        }

        public override bool Equals(object obj)
        {
            return obj is IzvodjackaGrupa grupa &&
                   IdIzvodjackaGrupa == grupa.IdIzvodjackaGrupa &&
                   Naziv == grupa.Naziv;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdIzvodjackaGrupa, Naziv);
        }
    }
}
