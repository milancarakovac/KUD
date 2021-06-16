using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface SponzorUplataDao
    {
        public List<SponzorUplata> GetAll();
        public SponzorUplata GetById(int id);
        public int Insert(SponzorUplata sponzorUplata);
        public int Update(SponzorUplata sponzorUplata);
        public int Delete(int id);
    }
}
