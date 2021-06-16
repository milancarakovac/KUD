using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface PrijateljKUDaDao
    {
        public List<PrijateljKUDa> GetAll();
        public PrijateljKUDa GetById(int id);
        public int Insert(PrijateljKUDa prijateljKUDa);
        public int Update(PrijateljKUDa prijateljKUDa);
        public int Delete(int id);
    }
}
