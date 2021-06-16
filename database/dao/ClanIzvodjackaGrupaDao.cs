using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface ClanIzvodjackaGrupaDao
    {
        public List<ClanIzvodjackaGrupa> GetAll();
        public ClanIzvodjackaGrupa GetById(int id);
        public int Insert(ClanIzvodjackaGrupa clanIzvodjackaGrupa);
        public int Update(ClanIzvodjackaGrupa clanIzvodjackaGrupa);
        public int Delete(int id);
    }
}
