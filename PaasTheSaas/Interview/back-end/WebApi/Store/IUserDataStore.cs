using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApi.Store
{
    public interface IUserDataStore
    {
        Task Connect(string connectionInfo);
        Task Delete(string id);
        Task<IEnumerable<T>> Get<T>();
        Task<T> Get<T>(string id);
        Task<string> Insert<T>(T document);
        Task<IEnumerable<T>> Query<T>(Expression<Func<T, bool>> predicate);
        Task Update<T>(string id, T document);
    }
}
