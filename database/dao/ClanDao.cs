using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface ClanDao
    {
        public List<Clan> GetAll();
        public Clan GetById(int id);
        public int Insert(Clan clan);
        public int Update(Clan clan);
        public int Delete(int id);
    }
}
