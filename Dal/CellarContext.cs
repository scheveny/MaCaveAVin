﻿using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class CellarContext : IdentityDbContext<AppUser>
    {
        public CellarContext(DbContextOptions<CellarContext> options) : base(options)
        {
        }
        #region DbSets
        //public DbSet<User> Users { get; set; }
        public DbSet<Bottle> Bottles { get; set; }
        public DbSet<Cellar> Cellars { get; set; }
        public DbSet<CellarCategory> CellarCategories { get; set; }
        public DbSet<CellarModel> CellarModels { get; set; }
        #endregion

        #region Constructors

        public CellarContext()
            : base()
        {
        }

        #endregion

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CellarDatabase;Integrated Security=true;");

            base.OnConfiguring(optionsBuilder);
        }
        #endregion
    }
}
