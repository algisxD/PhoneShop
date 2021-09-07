using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.Entities
{
    public class Saskaita
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Netinkama kaina; Daugiausiai du skaičiai po kalbelio.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Netinkama kaina; Daugiausiai 18 skaitmenų.")]
        public decimal Suma { get; set; }
        public DateTime? ApmokejimoData { get; set; }
        public DateTime ApmokejimoTerminas { get; set; }
        public Busena Busena { get; set; }
        public int UzsakymasId { get; set; }
        public Uzsakymas Uzsakymas { get; set; }
    }
    public enum Busena
    {
        Apmoketa = 0,
        Neapmoketa = 1,
        Veluojanti = 2
    }
}
