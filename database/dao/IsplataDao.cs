using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface IsplataDao
    {
        public List<Isplata> GetAll();
        public Isplata GetById(int id);
        public int Insert(Isplata isplata);
        public int Update(Isplata isplata);
        public int Delete(int id);
    }
}
