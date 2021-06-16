using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface SponzorDao
    {
        public List<Sponzor> GetAll();
        public Sponzor GetById(int id);
        public int Insert(Sponzor sponzor);
        public int Update(Sponzor sponzor);
        public int Delete(int id);
    }
}
