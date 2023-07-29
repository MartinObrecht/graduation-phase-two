﻿using Microsoft.EntityFrameworkCore;
using TechBlog.NewsManager.API.Domain.Entities;

namespace TechBlog.NewsManager.API.Infrastructure.Database.Context
{
    public sealed class SqlServerContext : DbContext, IDatabaseContext
    {
        public DbSet<BlogNew> BlogNews { get; set; }

        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> AnyPendingMigrationsAsync()
        {
            try
            {
                var migrations = await Database.GetPendingMigrationsAsync();

                return migrations.Any();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }

        public async Task TestConnectionAsync()
        {
            _ = await Database.ExecuteSqlRawAsync("SELECT 1");
        }
    }
}
