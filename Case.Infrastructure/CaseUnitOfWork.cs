using Common.Domain.Repositories;
using System.Collections;
using Case.Infrastructure.Data;
using Case.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Case.Domain.Repositories;

namespace Case.Infrastructure;

internal sealed class CaseUnitOfWork : ICaseUnitOfWork 
{
    private readonly CaseDbContext _context;
    private Hashtable _repositories;

    public CaseUnitOfWork(CaseDbContext context)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (_repositories is null)
            _repositories = new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repository = new GenericRepository<TEntity>(_context);
            _repositories.Add(type, repository);
        }

        return (IGenericRepository<TEntity>)_repositories[type]!;
    }
}
