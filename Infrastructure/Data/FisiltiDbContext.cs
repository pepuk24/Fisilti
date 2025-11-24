using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;


namespace Infrastructure.Data
{
    public class FisiltiDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public FisiltiDbContext()
        {

        }
        public FisiltiDbContext(DbContextOptions<FisiltiDbContext> options):base(options)
        {
            
        }

        //Veri tabanına bağlanıp Konfigürasyon İşlemleri Yapılırken Çalışan Method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=.;Database=FisiltiDB;Trusted_Connection=True;TrustServerCertificate=true;");
            base.OnConfiguring(optionsBuilder);
        }

        //Veritabanına bağlanıp kod tarafındaki classların tablo olarak oluşturulurken çalışan method.
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //IEntityTypeConfiguration<> interfaceinden kalıtım almış olan uygulama içerisindeki bütün konfigurasyonları otomatik olarak yakalayıp uyguluyor.
            var configurationAssembly = Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configurationAssembly);



            //BaseEntity tipindeki entityleri verir.
            var entityler = builder.Model.GetEntityTypes().Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType));

            foreach (var item in entityler)
            {
                //Aşağıdaki kodlarda yapılan işlemin kısa hali
                //e => e.IsActive == true

                //Sadece IsActive değeri true olanları dönecek
                var parameter = Expression.Parameter(item.ClrType, "e");
                var prop = Expression.Property(parameter, nameof(BaseEntity.IsActive));
                var condition = Expression.Equal(prop, Expression.Constant(true));
                var lambda = Expression.Lambda(condition, parameter);

                builder.Entity(item.ClrType).HasQueryFilter(lambda);

                //Ben bu şekilde yazıcam
                //select * from TabloAdı

                //bu filtre sorgunun sonuna IsActive değeri 1 olanları yan itrue olanları koşulunu ekleyecek ve sadce aktif olanları getirecek.
                //select * from TabloAdı where IsActive = 1


            }

            base.OnModelCreating(builder);
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
                if(e.State == EntityState.Modified)
                {
                    //güncellenirken Güncellenme zamanı otomatik olarak O anki tarih verilsin.
                    e.Entity.UpdatedDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        //DbSet: Veritabanı tablosu

        //Bu kısım uygulama içerisindeki classların veritabanına tablo olarak ekleneceği ve hangi isimle ekleneceğini belirttiğimiz bölüm.
        public DbSet<Prompt> Prompts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }




    }
}
