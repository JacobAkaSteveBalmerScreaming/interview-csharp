using Microsoft.Extensions.Caching.Memory;
using System;

namespace RestApi
{
    public class FakeDatabase
    {
        private readonly MemoryCache _db;
        public FakeDatabase()
        {
            _db = new MemoryCache(
                new MemoryCacheOptions
                {
                    SizeLimit = 100000
                }
            );
        }

        public bool Add()
        {
            throw new NotImplementedException();
        }

        public object Get()
        {
            throw new NotImplementedException();
        }
    }
}
