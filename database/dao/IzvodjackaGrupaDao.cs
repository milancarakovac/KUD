using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface IzvodjackaGrupaDao
    {
        public List<IzvodjackaGrupa> GetAll();
        public IzvodjackaGrupa GetById(int id);
        public int Insert(IzvodjackaGrupa izvodjackaGrupa);
        public int Update(IzvodjackaGrupa izvodjackaGrupa);
        public int Delete(int id);
    }
}
