using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUD.Tables
{
    class EquipmentCharge
    {
        public string Member { get; set; }
        public string Equipment { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public bool Discharged { get; set; }
        public int Id { get; set; }

        public EquipmentCharge(string member, string equipment, DateTime dateFrom, DateTime dateTo, bool discharged, int id)
        {
            Member = member;
            Equipment = equipment;
            DateFrom = dateFrom.ToString("dd.MM.yyyy.");
            DateTo = dateTo != DateTime.MinValue ? dateTo.ToString("dd.MM.yyyy.") : "";
            Discharged = discharged;
            Id = id;
        }
    }
}
