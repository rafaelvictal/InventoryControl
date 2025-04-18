using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryControl.Infra.Interfaces;

public interface IRepository<T> where T : class
{
    Task Add(T entity);
    Task Update(T entity);
    Task Remove(T entity);
    Task<T> GetOneById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetAll(string? includeProperties);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, string? includeProperties = null);
}