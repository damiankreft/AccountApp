using System;
using AccountApp.Infrastructure.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace AccountApp.Infrastructure.Extensions
{
    public static class CacheExtentions
    {
        public static void SetJwt(this IMemoryCache cache, Guid tokenId, JwtDto jwt)
            => cache.Set(GetJwtKey(tokenId), jwt, TimeSpan.FromSeconds(5));

        public static JwtDto GetJwt(this IMemoryCache cache, Guid tokenId)
            => cache.Get<JwtDto>(GetJwtKey(tokenId));

        public static string GetJwtKey(Guid tokenId)
            => $"jwt-{tokenId}";
    }
}