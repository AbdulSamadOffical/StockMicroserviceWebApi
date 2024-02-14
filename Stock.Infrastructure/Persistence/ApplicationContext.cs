using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Stock.Domain.Entities;

namespace Stock.Infrastructure.Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
   
        public DbSet<User> User { get; set; }
        public DbSet<StockProduct> StockProduct { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyCommonEntityConfiguration(modelBuilder);

        }

        private static void ApplyCommonEntityConfiguration(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().ToList();

                var createdAtProperty = properties.FirstOrDefault(p => p.Name == "CreatedAt");
                if (createdAtProperty != null)
                {
                    createdAtProperty.ValueGenerated = ValueGenerated.OnAdd;
                    createdAtProperty.SetDefaultValueSql("GETDATE()");
                }

                var updatedAtProperty = properties.FirstOrDefault(p => p.Name == "UpdatedAt");
                if (updatedAtProperty != null)
                {
                    updatedAtProperty.ValueGenerated = ValueGenerated.OnAddOrUpdate;
                    updatedAtProperty.SetDefaultValueSql("GETDATE()");
                }

                var deletedAtProperty = properties.FirstOrDefault(p => p.Name == "DeletedAt");
                if (deletedAtProperty != null)
                {
                    deletedAtProperty.SetDefaultValue(null);
                }
            }
        }
    }
}
