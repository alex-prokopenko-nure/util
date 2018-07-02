﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ng.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExcursionSight>()
                .HasKey(es => new { es.ExcursionId, es.SightId });
        }

        public void ClearDatabase()
        {
            Tours.RemoveRange(Tours.ToList());
            Clients.RemoveRange(Clients.ToList());
            Excursions.RemoveRange(Excursions.ToList());
            ExcursionSights.RemoveRange(ExcursionSights.ToList());
            Sights.RemoveRange(Sights.ToList());
        }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Sight> Sights { get; set; }
        public DbSet<Excursion> Excursions { get; set; }
        public DbSet<ExcursionSight> ExcursionSights { get; set; }
    }
}
