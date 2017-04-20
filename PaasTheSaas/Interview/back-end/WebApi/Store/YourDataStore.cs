
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Store
{
    public class YourDataStore : IDataStore
    {
        public Task Connect(string connectionInfo)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> Get<T>(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Insert<T>(T document)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Query<T>(string query)
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(T document)
        {
            throw new NotImplementedException();
        }
    }
}
