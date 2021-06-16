using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface GostDao
    {
        public List<Gost> GetAll();
        public Gost GetById(int id);
        public int Insert(Gost gost);
        public int Update(Gost gost);
        public int Delete(int id);
    }
}
