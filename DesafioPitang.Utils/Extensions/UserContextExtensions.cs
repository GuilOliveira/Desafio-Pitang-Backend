using DesafioPitang.Utils.UserContext;
using System.Collections;
using System.Security.Claims;

namespace DesafioPitang.Utils.Extensions
{
    public static class UserContextExtensions
    {
        public static string Login(this IUserContext userContext)
        {
            var login = userContext.GetClaimValue<string>("login");

            return login ?? Environment.MachineName;
        }

        public static int Id(this IUserContext userContext)
        {
            int.TryParse(userContext.GetClaimValue<string>(ClaimTypes.Sid), out var id);

            return id;
        }

        public static void AddData<TValue>(this IUserContext userContext, string key, TValue data)
        {
            userContext.AdditionalData ??= new Hashtable();

            if (!userContext.AdditionalData.ContainsKey(key))
                userContext.AdditionalData.Add(key, data);
            else
                userContext.AdditionalData[key] = data;
        }

        private static TResult? GetClaimValue<TResult>(this IUserContext userContext, string key)
        {
            if (userContext?.AdditionalData is Hashtable additionalData && additionalData.ContainsKey(key))
                try { return (TResult)additionalData[key]; } catch { return default; }

            return default;
        }
    }
}
