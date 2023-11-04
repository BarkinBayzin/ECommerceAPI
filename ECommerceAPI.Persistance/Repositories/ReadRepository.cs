using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ECommerceAPIContext _context;
    public ReadRepository(ECommerceAPIContext context) {_context = context;}
    public DbSet<T> Table => _context.Set<T>();
    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.Where(x => x.Status != Status.Passive).AsQueryable();
        if(!tracking) 
            query = query.AsNoTracking();
        return query;
    }
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true)
    {
        var query = Table.Where(expression);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
        
    }
    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)
    {
        var query = Table.Where(x => x.Status != Status.Passive).AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(expression);
        
    }
    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    //=> await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    //=> await Table.FindAsync(Guid.Parse(id));
    {
        var query = Table.Where(x => x.Status != Status.Passive).AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
    }

    public IQueryable<T> GetPassives(bool tracking = true)
    {
        var query = Table.Where(x => x.Status == Status.Passive).AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }
}
