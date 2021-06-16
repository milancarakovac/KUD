using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface IzvodjackaGrupaNastupDao
    {
        public List<IzvodjackaGrupaNastup> GetAll();
        public IzvodjackaGrupaNastup GetById(int idIzvodjackaGrupa, int idNastup);
        public int Insert(IzvodjackaGrupa izvodjackaGrupa, Nastup nastup);
        public int Update(IzvodjackaGrupaNastup izvodjackaGrupaNastup);
        public int Delete(int idIzvodjackaGrupa, int idNastup);
    }
}
