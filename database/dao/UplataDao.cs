using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface UplataDao
    {
        public List<Uplata> GetAll();
        public Uplata GetById(int id);
        public int Insert(Uplata uplata);
        public int Update(Uplata uplata);
        public int Delete(int id);
    }
}
