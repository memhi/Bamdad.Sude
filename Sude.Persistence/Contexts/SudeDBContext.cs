using Sude.Domain.Models.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Serving;
using Sude.Domain.Models.Work;
using Sude.Domain.Models.Order;

namespace Sude.Persistence.Contexts
{
    public class SudeDBContext : DbContext
    {
        public SudeDBContext(DbContextOptions<SudeDBContext> options)
            :base(options)
        {

        }

        public DbSet<Serving> Servings { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Serving>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<Work>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsRemoved);

            //modelBuilder.Entity<Role>().HasData(new Role() { Id = 1, Title = "admin" }, new Role() { Id = 2, Title = "Operator" });
        }

    }
}
