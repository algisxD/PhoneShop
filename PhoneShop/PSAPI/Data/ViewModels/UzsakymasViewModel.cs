using AutoMapper;
using PSAPI.Data.Entities;
using PSAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.ViewModels
{
    public class UzsakymasViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public Tipas UzsakymoTipas { get; set; }
        public DateTime Data { get; set; }
        public SaskaitosBusena ApmokejimoBusena { get; set; }
        public string KlientoId { get; set; }
        public int TelefonoModelisId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Uzsakymas, UzsakymasViewModel>();
        }
    }
}
