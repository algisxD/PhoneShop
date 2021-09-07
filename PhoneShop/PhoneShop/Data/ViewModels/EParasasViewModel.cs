using AutoMapper;
using PSAPI.Data.Entities;
using PSAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.ViewModels
{
    public class EParasasViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public int Pin { get; set; }
        public int PlanasId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<EParasas, EParasasViewModel>();
        }
    }
}
