using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Shop.Common.Extensions    
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.Set(key, JsonSerializer.Serialize<T>(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            session.TryGetValue(key, out byte[] outValue);
            return outValue == null ? default(T) : JsonSerializer.Deserialize<T>(outValue);
        }
    }
}
