using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface KoncertDao
    {
        public List<Koncert> GetAll();
        public Koncert GetById(int id);
        public int Insert(Koncert koncert);
        public int Update(Koncert koncert);
        public int Delete(int id);
    }
}
