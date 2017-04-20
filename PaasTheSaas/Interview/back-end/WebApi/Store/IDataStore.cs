
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Store
{
    public interface IDataStore
    {
        Task Connect(string connectionInfo);
        Task Delete(string id);
        Task<IEnumerable<T>> Get<T>();
        Task<T> Get<T>(string id);
        Task<string> Insert<T>(T document);
        Task<IEnumerable<T>> Query<T>(string query);
        Task Update<T>(T document);
    }
}
