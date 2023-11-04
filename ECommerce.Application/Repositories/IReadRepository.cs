using System.Linq.Expressions;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
{
    //Sorgu üzerinde çalışmak istiyorsak IQueryable kullanarak daha optimize ediyoruz, List'de bir IEnumarable InMemoryde çalışacaktır.
    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true);
    IQueryable<T> GetPassives(bool tracking = true);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true);
    Task<T> GetByIdAsync(string id, bool tracking = true);
}
