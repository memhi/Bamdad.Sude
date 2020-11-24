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
using Sude.Domain.Models.PushNotification;
using Sude.Domain.Models.Content;

namespace Sude.Persistence.Contexts
{
    public class SudeDBContext : DbContext
    {
        public SudeDBContext(DbContextOptions<SudeDBContext> options)
            :base(options)
        {

        }


        public DbSet<WorkTypeInfo> WorkTypes { get; set; }
        public DbSet<WorkInfo> Works { get; set; }
        public DbSet<ServingInfo> Servings { get; set; }
        public DbSet<OrderInfo> Orders { get; set; }
        public DbSet<OrderDetailInfo>  OrderDetails { get; set; }        
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<RoleInfo> Roles { get; set; }
      
        public DbSet<UserDeviceInfo> UserDevices { get; set; }
        public DbSet<ServingInventoryInfo> ServingInventories { get; set; }
        public DbSet<InventoryTypeInfo> InventoryTypes { get; set; }
        public DbSet<ServingInventoryTrackingInfo> ServingInventoryTrackings { get; set; }
        public DbSet<BlogInfo> Blogs { get; set; }
        public DbSet<BlogCommentInfo> BlogComments { get; set; }


        public DbSet<NewsInfo>   News { get; set; }
        public DbSet<NewsCommentInfo>  NewsComments { get; set; }

        public DbSet<CustomerInfo> Customers { get; set; }
        public DbSet<AddressInfo>  Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServingInfo>().HasQueryFilter(p=>!p.IsRemoved);
            modelBuilder.Entity<WorkInfo>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<OrderInfo>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ServingInventoryInfo>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ServingInventoryTrackingInfo>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CustomerInfo>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<NewsInfo>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<NewsCommentInfo>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<BlogInfo>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<BlogCommentInfo>().HasQueryFilter(p => !p.IsRemoved);

            //modelBuilder.Entity<Role>().HasData(new Role() { Id = 1, Title = "admin" }, new Role() { Id = 2, Title = "Operator" });
        }

    }
}
