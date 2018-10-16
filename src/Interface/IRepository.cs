using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeSquirl.Recipy.DataProvider
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> entities) where T : class;
        bool Remove<T>(T entiy) where T : class;
        int Remove<T>(Func<T, bool> predecate) where T : class;
        IQueryable<T> Get<T>() where T : class;
        IQueryable<T> Get<T>(Func<T, bool> predicate) where T : class;
        bool Update<T>(T entity) where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken token);
    }
}