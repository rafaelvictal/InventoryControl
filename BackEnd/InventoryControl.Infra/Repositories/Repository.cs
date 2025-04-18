using InventoryControl.Infra.Data;
using InventoryControl.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryControl.Infra.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task Add(T entity) => await _dbSet.AddAsync(entity);

    public async Task Remove(T entity) => _dbSet.Remove(entity);

    public async Task Update(T entity) => _dbSet.Update(entity);

    public async Task<T> GetOneById(int id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

    public async Task<IEnumerable<T>> GetAll(string? includeProperties)
    {
        var query = ApplyIncludes(_dbSet, includeProperties);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, string? includeProperties = null)
    {
        var query = ApplyIncludes(_dbSet.Where(predicate), includeProperties);
        return await query.ToListAsync();
    }

    private IQueryable<T> ApplyIncludes(IQueryable<T> query, string? includeProperties)
    {
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var prop in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop.Trim());
            }
        }

        return query;
    }
}
