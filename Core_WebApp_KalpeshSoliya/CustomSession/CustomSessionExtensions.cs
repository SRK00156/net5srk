using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core_WebApp_KalpeshSoliya.CustomSession
{
    /// <summary>
    /// The Extetion class that contain methods for the session management of CLR object in JSON form.
    /// </summary>
    public static class CustomSessionExtensions
    {
        ///<summary>
        /// Extension method for ISession
        /// </summary>
        /// <Typeparam name="T"></Typeparam>
        /// <Param name="session"></Param>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            //The CLR object will be JSON serialized and store in session
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            //read data from session
            var data = session.GetString(key);
            if (data == null) return default(T); //Return empty instance of CLR object
            //Deserialize the CLR object and return
            return JsonSerializer.Deserialize<T>(data);
        }
    }
}
