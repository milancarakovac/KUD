using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface NastupDao
    {
        public List<Nastup> GetAll();
        public Nastup GetById(int id);
        public int Insert(Nastup nastup);
        public int Update(Nastup nastup);
        public int Delete(int id);
    }
}
