using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PSAPI.Data.Entities;

namespace PSAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Detale> Detales { get; set; }
        public DbSet<TelefonoModelis> TelefonoModeliai { get; set; }
        public DbSet<Darbuotojas> Darbuotojai { get; set; }
        public DbSet<Planas> Planai { get; set; }
        public DbSet<Saskaita> Saskaitos { get; set; }
        public DbSet<Uzsakymas> Uzsakymai { get; set; }
        public DbSet<EParasas> EParasai { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
