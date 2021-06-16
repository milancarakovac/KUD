using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao
{
    interface DonacijaDao
    {
        public List<Donacija> GetAll();
        public Donacija GetById(int id);
        public int Insert(Donacija donacija);
        public int Update(Donacija donacija);
        public int Delete(int id);
    }
}
