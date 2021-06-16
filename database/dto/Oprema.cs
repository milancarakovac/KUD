using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class Oprema
    {
        public int IdOprema { get; set; }
        public string Naziv { get; set; }
        public string SerijskiBroj { get; set; }
        public string Opis { get; set; }
        public bool Obrisan { get; set; }
        public bool Zaduzen { get; set; }

        public Oprema(int idOprema, string naziv, string serijskiBroj, string opis, bool obrisan, bool zaduzen)
        {
            IdOprema = idOprema;
            Naziv = naziv;
            SerijskiBroj = serijskiBroj;
            Opis = opis;
            Obrisan = obrisan;
            Zaduzen = zaduzen;
        }

        public override bool Equals(object obj)
        {
            return obj is Oprema oprema &&
                   IdOprema == oprema.IdOprema &&
                   Naziv == oprema.Naziv &&
                   SerijskiBroj == oprema.SerijskiBroj &&
                   Opis == oprema.Opis;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdOprema, Naziv, SerijskiBroj, Opis);
        }

        public override string ToString()
        {
            return Naziv + " (" + SerijskiBroj + ")";
        }
    }
}
