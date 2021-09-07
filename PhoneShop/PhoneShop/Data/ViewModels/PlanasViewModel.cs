using AutoMapper;
using PSAPI.Data.Entities;
using PSAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.ViewModels
{
    public class PlanasViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Pavadinimas { get; set; }
        public decimal MenMokestis { get; set; }
        public int GaliojimoLaikas { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Planas, PlanasViewModel>();
        }
    }
}
