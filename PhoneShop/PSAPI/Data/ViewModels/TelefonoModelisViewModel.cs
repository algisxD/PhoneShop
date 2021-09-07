using AutoMapper;
using PSAPI.Data.Entities;
using PSAPI.Data.Interfaces;
using System;


namespace PSAPI.Data.ViewModels
{
    public class TelefonoModelisViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Pavadinimas { get; set; }
        public string Gamintojas { get; set; }
        public DateTime IsleidimoData { get; set; }
        public decimal Kaina { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TelefonoModelis, TelefonoModelisViewModel>();
        }
    }
}
