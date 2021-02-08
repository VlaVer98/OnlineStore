using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace Shop.WEB.Core.Extensions.Session
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.Set(key, Encoding.Default.GetBytes(JsonSerializer.Serialize<T>(value)));
        }

        public static T Get<T>(this ISession session, string key)
        {
            session.TryGetValue(key, out byte[] outValue);
            return outValue == null ? default(T) : JsonSerializer.Deserialize<T>(outValue);
        }
    }
}
