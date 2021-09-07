using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.Entities
{
    public class Uzsakymas
    {
        public int Id { get; set; }
        public Tipas UzsakymoTipas { get; set; }
        public DateTime Data { get; set; }
        public SaskaitosBusena ApmokejimoBusena { get; set; }
        public string KlientoId { get; set; }
        public int TelefonoModelisId { get; set; }
        public TelefonoModelis TelefonoModelis { get; set; }
    }
    public enum SaskaitosBusena
    {
        Nepatvirtinta = 0,
        Patvirtinta = 1,
        Atšaukta = 2,
        Užbaigta = 3,
    }
    public enum Tipas
    {
        Pirkimas = 0,
        Remontas = 1,
        Grazinimas = 2
    }
}
