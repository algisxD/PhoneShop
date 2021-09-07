using AutoMapper;
using PSAPI.Data.Entities;
using PSAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSAPI.Data.ViewModels
{
    public class SaskaitaViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public decimal Suma { get; set; }
        public DateTime? ApmokejimoData { get; set; }
        public DateTime ApmokejimoTerminas { get; set; }
        public Busena Busena { get; set; }
        public string KlientoId { get; set; }
        public int UzsakymasId { get; set; }
        public DateTime UsakymasData { get; set; }
        public string TelefonoId { get; set; }
        public string TelefonoPavadinimas { get; set; }
        public decimal TelefonoKaina { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Saskaita, SaskaitaViewModel>()
                .ForMember(e => e.TelefonoId, opt => opt.MapFrom(p => p.Uzsakymas.TelefonoModelis.Id))
                .ForMember(e => e.TelefonoKaina, opt => opt.MapFrom(p => p.Uzsakymas.TelefonoModelis.Kaina))
                .ForMember(e => e.TelefonoPavadinimas, opt => opt.MapFrom(p => p.Uzsakymas.TelefonoModelis.Pavadinimas))
                .ForMember(e => e.UsakymasData, opt => opt.MapFrom(p => p.Uzsakymas.Data))
                .ForMember(e => e.KlientoId, opt => opt.MapFrom(p => p.Uzsakymas.KlientoId));
        }
    }
}
