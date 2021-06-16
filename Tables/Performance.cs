using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUD.Tables
{
    class Performance
    {
        public string Datum { get; set; }
        public string Mjesto { get; set; }
        public string Naziv { get; set; }
        public int Id { get; set; }

        public Performance(string datum, string mjesto, string naziv, int id)
        {
            Datum = datum;
            Mjesto = mjesto;
            Naziv = naziv;
            Id = id;
        }
    }
}
