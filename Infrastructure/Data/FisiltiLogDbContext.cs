using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class FisiltiLogDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=FisiltiLogDB;Trusted_Connection=True;TrustServerCertificate=true;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



        //Kayıt işlemi yapılırken araya girip eğer yeni veri ekleniyorsa Eklenme Tarihini, eğer veri güncelleniyorsa güncelleme tarihini değiştiriyoruz.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var e in entries)
            {
                //Yeni bir veri ekleniyorsa
                if (e.State == EntityState.Added)
                {
                    //Eklenirken Aktif değeri otomatik olarak true olarak eklensin
                    e.Entity.IsActive = true;

                    //Eklenirken Eklenme zamanı otomatik olarak O anki tarih verilsin.
                    e.Entity.CreatedDate = DateTime.Now;
                }

                //Veri Güncelleniyorsa
                if (e.State == EntityState.Modified)
                {
                    //güncellenirken Güncellenme zamanı otomatik olarak O anki tarih verilsin.
                    e.Entity.UpdatedDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<AuditLog> AuditLogs { get; set; }

    }
}
