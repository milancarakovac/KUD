using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface OpremaDao
    {
        public List<Oprema> GetAll();
        public Oprema GetById(int id);
        public int Insert(Oprema oprema);
        public int Update(Oprema oprema);
        public int Delete(int id);
    }
}
