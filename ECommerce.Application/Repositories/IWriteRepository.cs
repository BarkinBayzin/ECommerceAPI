using Microsoft.EntityFrameworkCore;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(List<T> data);
    Task<bool> RemoveAsync(string id);
    Task<bool> Remove(T entity);
    bool Destroy(T entity);
    bool DestroyRange(List<T> data);
    Task<bool> Update(T entity, EntityState entityState = EntityState.Modified);
    Task<int> SaveAsync();
}
