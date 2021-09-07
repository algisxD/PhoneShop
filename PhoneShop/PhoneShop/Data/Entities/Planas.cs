using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.Entities
{
    public class Planas
    {
        public int Id { get; set; }
        public string Pavadinimas { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Netinkama kaina; Daugiausiai du skaičiai po kalbelio.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Netinkama kaina; Daugiausiai 18 skaitmenų.")]
        public decimal MenMokestis { get; set; }
        public int GaliojimoLaikas { get; set; }
    }
}
