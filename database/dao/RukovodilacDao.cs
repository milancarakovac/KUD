using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface RukovodilacDao
    {
        public List<Rukovodilac> GetAll();
        public Rukovodilac GetById(int id);
        public int Insert(Rukovodilac rukovodilac);
        public int Update(Rukovodilac rukovodilac);
        public int Delete(int id);
    }
}
