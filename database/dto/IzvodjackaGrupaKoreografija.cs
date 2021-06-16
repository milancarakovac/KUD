using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class IzvodjackaGrupaKoreografija
    {
        public int IdIzvodjackaGrupa { get; set; }
        public int IdKoreografija { get; set; }

        public IzvodjackaGrupaKoreografija(int idIzvodjackaGrupa, int idKoreografija)
        {
            IdIzvodjackaGrupa = idIzvodjackaGrupa;
            IdKoreografija = idKoreografija;
        }

        public override bool Equals(object obj)
        {
            return obj is IzvodjackaGrupaKoreografija koreografija &&
                   IdIzvodjackaGrupa == koreografija.IdIzvodjackaGrupa &&
                   IdKoreografija == koreografija.IdKoreografija;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdIzvodjackaGrupa, IdKoreografija);
        }
    }
}
