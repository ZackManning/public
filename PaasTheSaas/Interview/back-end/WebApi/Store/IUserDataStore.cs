using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Store
{
    public interface IUserDataStore
    {
        Task Connect(string connectionInfo);
        Task Delete(string id);
        Task<IEnumerable<T>> Get<T>();
        Task<T> Get<T>(string id);
        Task<T> Insert<T>(T document);
        Task<IEnumerable<T>> Query<T>(Func<T, bool> predicate);
        Task Update<T>(string id, T document);
    }
}
