using Microsoft.EntityFrameworkCore;
using NetPower.Application.Common.Interfaces;
using NetPower.Domain.Entities.Common;
using NetPower.Domain.Interfaces.Repositories;
using NetPower.Infrastructure.Persistence.Contexts;

namespace NetPower.Infrastructure.Persistence.Repositories.EfCore;

/// <summary>
/// Generic repository implementation using Entity Framework Core.
/// </summary>
public class GenericRepository<T> : IGenericRepository<T> where T : AuditableEntity, IAggregateRoot
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
