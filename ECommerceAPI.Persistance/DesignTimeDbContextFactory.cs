using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

//.NET CLI üzerinden çalışmak istenen durumlarda migration hatalarının çözümlemek için kullanılır
// dotnet ef migrations add mig_1
// dotnet ef database update
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIContext>
{
    public ECommerceAPIContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ECommerceAPIContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
        return new(dbContextOptionsBuilder.Options);
    }
}
