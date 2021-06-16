using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface BlagajnikDao
    {
        public List<Blagajnik> GetAll();
        public Blagajnik GetById(int id);
        public int Insert(Blagajnik blagajnik);
        public int Update(Blagajnik blagajnik);
        public int Delete(int id);
    }
}
