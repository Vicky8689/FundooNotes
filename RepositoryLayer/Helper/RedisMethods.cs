using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RepositoryLayer.Helper
{

    public class RedisMethods
    {
        private IDistributedCache _cache;

        public RedisMethods(IDistributedCache cache)
        {
            _cache = cache;
        }

        public   void SetCache(string key, List<GetAllNotesResponseModel> value)
        {
            //set expirtime
            var expirTime = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(10)
            };

            _cache.SetString(key, JsonSerializer.Serialize(value),expirTime);

        }
        public  void RemoveCache(string key)
        {
            _cache.Remove(key);
        }

    }
}
