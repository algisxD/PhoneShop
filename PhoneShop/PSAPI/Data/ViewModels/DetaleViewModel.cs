using AutoMapper;
using PSAPI.Data.Entities;
using PSAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.ViewModels
{
    public class DetaleViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Pavadinimas { get; set; }
        public decimal Savikaina { get; set; }
        public DateTime PagaminimoData { get; set; }
        public string KilmesSalis { get; set; }
        public string SerijosNumeris { get; set; }
        public int TelefonoModelisId { get; set; }

        public string TelefonoModelioName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Detale, DetaleViewModel>()
                .ForMember(e => e.TelefonoModelioName, opt => opt.MapFrom(p => p.TelefonoModelis.Pavadinimas));
        }
    }
}
