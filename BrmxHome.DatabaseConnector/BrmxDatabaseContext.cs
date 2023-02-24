using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using BrmxHome.DatabaseConnector.Models;


namespace BrmxHome.DatabaseConnector
{
    public class BrmxDatabaseContext: DbContext
    {
        public DbSet<Sensor> Sensors { get; set; }
        // public string DbPath { get; }

        public BrmxDatabaseContext(DbContextOptions<BrmxDatabaseContext> options): base(options)
        {
            Database.EnsureCreated();
            Database.Migrate();
            // _configuration = configuration;
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = System.IO.Path.Join(path, "reporterDB.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite($"Data Source=./App_Data/brmxDb.db");
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var createdEntries = ChangeTracker
                .Entries()
                .Where(e => typeof(AuditedEntity).IsAssignableFrom(e.Entity.GetType()));

            foreach (var entityEntry in createdEntries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((AuditedEntity)entityEntry.Entity).CreationTime = DateTime.UtcNow;
                }
            }

            var updatedEntries = ChangeTracker
                .Entries()
                .Where(e => typeof(UpdatedEntity).IsAssignableFrom(e.Entity.GetType()));

            foreach (var entityEntry in updatedEntries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((UpdatedEntity)entityEntry.Entity).Updated = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}