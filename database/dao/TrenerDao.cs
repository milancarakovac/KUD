using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface TrenerDao
    {
        public List<Trener> GetAll();
        public Trener GetById(int id);
        public int Insert(Trener trener);
        public int Update(Trener trener);
        public int Delete(int id);
    }
}
