using Microsoft.EntityFrameworkCore;
public class ECommerceAPIContext : DbContext
{
    public ECommerceAPIContext(DbContextOptions options) : base(options) { }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // ChangeTracker : Entityler üzerinden yapılan değişikliklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında track edilen verileri yakalayıp elde etmemizi sağlar.
        // Interceptor inşa ederek, kod tekrarına düşmüyoruz. CreateDate, UpdateDate, Status gibi durumları burda ele alabiliriz.

        foreach (var data in ChangeTracker.Entries<BaseEntity>())
        {
            if(data.State == EntityState.Added)
            {
                data.Entity.CreatedDate = DateTime.UtcNow;
                data.Entity.Status = Status.Active;
            }
            else if(data.State == EntityState.Modified)
            {
                data.Entity.UpdatedDate = DateTime.UtcNow;
                data.Entity.Status = Status.Modified;
            }
            else if(data.State == EntityState.Deleted)
            {
                data.Entity.DeletedDate = DateTime.UtcNow;
                data.Entity.Status = Status.Passive;
                data.State = EntityState.Modified;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
