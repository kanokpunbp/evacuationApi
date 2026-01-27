using evacuation.Application.DTOs.EvacuationPlan;
using evacuation.Application.Interfaces;
//using Microsoft.EntityFrameworkCore.Storage;
 

using StackExchange.Redis;
using System.Numerics;
//using RedisDatabase = StackExchange.Redis.IDatabase;

namespace evacuation.Infrastructure.Redis
{
    public class EvacuationStatusRedisCache : IEvacuationStatusCache
    {
        private readonly IDatabase _redis;
        private const string KeyPrefix = "evacuation:zone:";

        public EvacuationStatusRedisCache(IConnectionMultiplexer connection)
        {
            _redis = connection.GetDatabase();
        }

        private static string GetKey(string zoneCode) => $"{KeyPrefix}{zoneCode}";


        public async Task InitializeZoneAsync(string planId, string zoneCode, int totalPeople)
        {
            var key = GetKey(zoneCode);

            await _redis.HashSetAsync(key, new HashEntry[]
                    {
                new("planId", planId),
                new("total", totalPeople),
                new("evacuated", 0),
                new("remaining", totalPeople),
                new("lastVehicle", "")
                    });
        }

        public async Task UpdateAsync(string zoneCode, int evacuatedDelta, string? lastVehicle)
        {
            var key = GetKey(zoneCode);

            await _redis.HashIncrementAsync(key, "evacuated", evacuatedDelta);
            await _redis.HashIncrementAsync(key, "remaining", -evacuatedDelta);

            if (!string.IsNullOrEmpty(lastVehicle))
            {
                await _redis.HashSetAsync(key, "lastVehicle", lastVehicle);
            }
        }

        public async Task<IReadOnlyList<EvacuationStatusDto>> GetAllAsync()
        {
            var result = new List<EvacuationStatusDto>();

            var server = _redis.Multiplexer.GetServer(
                _redis.Multiplexer.GetEndPoints().First());

            foreach (var key in server.Keys(pattern: $"{KeyPrefix}*"))
            {
                var data = await _redis.HashGetAllAsync(key);

                if (data.Length == 0) continue;

                result.Add(new EvacuationStatusDto
                {
                    PlanId = data.First(x => x.Name == "planId").Value.ToString(),
                    ZoneCode = key.ToString().Replace(KeyPrefix, ""),
                    TotalPeople = (int)data.First(x => x.Name == "total").Value,
                    Evacuated = (int)data.First(x => x.Name == "evacuated").Value,
                    Remaining = (int)data.First(x => x.Name == "remaining").Value,
                    LastVehicleUsed = data.First(x => x.Name == "lastVehicle").Value
                });
            }

            return result;
        }
        public async Task ClearAsync()
        {
            var server = _redis.Multiplexer.GetServer(_redis.Multiplexer.GetEndPoints().First());

            foreach (var key in server.Keys(pattern: $"{KeyPrefix}*"))
            {
                await _redis.KeyDeleteAsync(key);
            }
        }

    }
}
