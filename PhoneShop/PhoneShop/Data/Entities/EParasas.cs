using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.Entities
{
    public class EParasas
    {
        public int Id { get; set; }
        public int Pin { get; set; }
        public int PlanasId { get; set; }
        public Planas Planas { get; set; }
    }
}
