using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface ClanOpremaDao
    {
        public List<ClanOprema> GetAll();
        public ClanOprema GetById(int id);
        public int Insert(ClanOprema clanOprema);
        public int Update(ClanOprema clanOprema);
        public int Delete(int id);
    }
}
