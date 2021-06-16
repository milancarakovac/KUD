using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface KoncertNastupDao
    {
        public List<KoncertNastup> GetAll();
        public KoncertNastup GetById(int idKoncert, int idNastup);
        public int Insert(Koncert koncert, Nastup nastup);
        public int Update(Koncert koncert, Nastup nastup);
        public int Delete(int idKoncert, int idNastup);
    }
}
