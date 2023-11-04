using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly ECommerceAPIContext _context;
    public WriteRepository(ECommerceAPIContext context) { _context = context; }
    public DbSet<T> Table => _context.Set<T>();
    public async Task<bool> AddAsync(T entity)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(entity);
        await SaveAsync();
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> data)
    {
        await Table.AddRangeAsync(data);
        if(await SaveAsync() > 0) return true;
        return false;
    }

    public async Task<bool> Remove(T entity)
    {
        bool result = await Update(entity, EntityState.Deleted);
        return result;
    }

    public async Task<bool> RemoveAsync(string id)
    {
        T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        return await Update(model, EntityState.Deleted);
    }
    public async Task<bool> Update(T entity, EntityState entityState)
    {
        EntityEntry<T> entityEntry = Table.Update(entity);
        if (entityState == EntityState.Deleted)
        {
            entityEntry.State = EntityState.Deleted;
            await SaveAsync();
            return entityState == EntityState.Deleted;
        }

        entityEntry.State = EntityState.Modified;
        await SaveAsync();
        return entityState == EntityState.Modified;
    }
    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public bool Destroy(T entity)
    {
        EntityEntry<T> entityEntry = Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool DestroyRange(List<T> data)
    {
        Table.RemoveRange(data);
        return true;
    }
}