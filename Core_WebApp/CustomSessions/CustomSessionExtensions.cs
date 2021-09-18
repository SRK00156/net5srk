using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core_WebApp.CustomSessions
{
	/// <summary>
	/// The Extension CLass that will contain methods for the Session Management of
	/// CLR Objects in JSON form
	/// </summary>
	public static class CustomSessionExtensions
	{
		/// <summary>
		///  Ectensiokn method for ISession
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="session"></param>
		/// <param name="key"></param>
		/// <param name="Value"></param>
		public static void SetObject<T>(this ISession session, string key, T Value)
		{
			// The CLR object will be JSON Serialized and Store in Session
			session.SetString(key, JsonSerializer.Serialize(Value));
		}

		public static T GetObject<T>(this ISession session, string key)
		{
			// read data from Session
			var data = session.GetString(key);
			if (data == null) return default(T); // return an empy instance of the CLR Object

			// Deserialize the CLR Object and return
			return JsonSerializer.Deserialize<T>(data);
		}
	}
}
