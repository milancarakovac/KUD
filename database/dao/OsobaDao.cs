using KUD.database.dto;
using System.Collections.Generic;

namespace KUD.database.dao
{
    interface OsobaDao
    {
        public List<Osoba> GetAll();
        public Osoba GetById(int id);
        public int Insert(Osoba osoba);
        public int Update(Osoba osoba);
        public int Delete(int id);
    }
}
