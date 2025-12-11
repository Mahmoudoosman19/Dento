using Common.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Repository_Abstraction;
using Task.Infrastructure.Data;
using Task.Infrastructure.Repositories;

namespace Task.Infrastructure
{
    internal sealed class TaskUnitOfWork : ITaskUnitOfWork
    {
        private readonly TaskDbContext _context;
        private Hashtable _repositories;

        public TaskUnitOfWork(TaskDbContext context)
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
                var repository = new TaskGenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }

            return (IGenericRepository<TEntity>)_repositories[type]!;
        }
    }
}
