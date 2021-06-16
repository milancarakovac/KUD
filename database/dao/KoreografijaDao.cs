using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface KoreografijaDao
    {
        public List<Koreografija> GetAll();
        public Koreografija GetById(int id);
        public int Insert(Koreografija koreografija);
        public int Update(Koreografija koreografija);
        public int Delete(int id);
    }
}
