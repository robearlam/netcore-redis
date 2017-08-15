using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;

namespace NetCoreRedis.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDistributedCache cache = new RedisCache(new OptionsWrapper<RedisCacheOptions>(new RedisCacheOptions
            {
                Configuration = "127.0.0.1:32769",
                InstanceName = "master"
            }));

            var prefix = "cacheKeyPrefix_";
            for (var i = 0; i <= 99; i++)
            {
                cache.SetString($"{prefix}{i}", DateTime.Now.Ticks.ToString());
                Console.WriteLine($"Writing to cache: {i}");
            }

            for (var i = 0; i <= 99; i++)
            {
                Console.WriteLine(cache.GetString($"{prefix}{i}"));
            }
        }
    }
}