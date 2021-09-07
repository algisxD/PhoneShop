using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.Entities
{
    public class TelefonoModelis
    {
        public TelefonoModelis()
        {
            Detales = new HashSet<Detale>();
            Uzsakymai = new HashSet<Uzsakymas>();
        }
        public int Id { get; set; }
        public string Pavadinimas { get; set; }
        public string Gamintojas { get; set; }
        public DateTime IsleidimoData { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Netinkama kaina; Daugiausiai du skaičiai po kalbelio.")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Netinkama kaina; Daugiausiai 18 skaitmenų.")]
        public decimal Kaina { get; set; }
        public ICollection<Detale> Detales { get; private set; }
        public ICollection<Uzsakymas> Uzsakymai { get; private set; }
    }
}
