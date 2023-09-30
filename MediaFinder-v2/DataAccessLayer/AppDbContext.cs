﻿using MediaFinder_v2.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaFinder_v2.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppSettingValue> AppSettings => Set<AppSettingValue>();
        public DbSet<SearchSettings> SearchSettings => Set<SearchSettings>();
        public DbSet<FileDetails> FileDetails => Set<FileDetails>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mediaFinder.db");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
