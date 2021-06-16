using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface NalogDao
    {
        public List<Nalog> GetAll();
        public Nalog GetById(int id);
        public int Insert(Nalog nalog);
        public int Update(Nalog nalog);
        public int Delete(int id);
    }
}
