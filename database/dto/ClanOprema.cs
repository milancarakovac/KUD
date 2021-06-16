using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dto
{
    class ClanOprema
    {
        public int IdClanOprema { get; set; }
        public int IdClan { get; set; }
        public int IdOprema { get; set; }
        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        public bool Razduzeno { get; set; }

        public ClanOprema(int idClanOprema, int idClan, int idOprema, DateTime datumOd, DateTime datumDo, bool razduzeno)
        {
            IdClanOprema = idClanOprema;
            IdClan = idClan;
            IdOprema = idOprema;
            DatumOd = datumOd;
            DatumDo = datumDo;
            Razduzeno = razduzeno;
        }

        public override bool Equals(object obj)
        {
            return obj is ClanOprema oprema &&
                   IdClanOprema == oprema.IdClanOprema &&
                   IdClan == oprema.IdClan &&
                   IdOprema == oprema.IdOprema &&
                   DatumOd == oprema.DatumOd &&
                   DatumDo == oprema.DatumDo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdClanOprema, IdClan, IdOprema, DatumOd, DatumDo);
        }
    }
}
