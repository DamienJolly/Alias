using System;
using System.Diagnostics;

namespace Alias.Emulator
{
    class Alias
    {
		private static AliasServer _server = null;

		private static DateTime _started = DateTime.Now;

		public static DateTime ServerStarted
		{
			get
			{
				return _started;
			}
		}

		/// <summary>
		/// Current Version of the server.
		/// </summary>
		public static string Version
		{
			get
			{
				return "0.6.6.3";
			}
		}

		/// <summary>
		/// Is a debugger currently attached to the server.
		/// </summary>
		public static bool IsDebugging
		{
			get
			{
				return Debugger.IsAttached;
			}
		}

		public Alias(string[] args)
		{
			_server = new AliasServer();
			_server.Initialize();
			GC.KeepAlive(_server);
		}
		
		public static AliasServer GetServer() => _server;
		
		/// <summary>
		/// Current server time in unix timestamp format.
		/// </summary>
		public static double GetUnixTimestamp()
		{
			return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
		}

		/// <summary>
		/// Converts a boolean into a string.
		/// </summary>
		/// <param name="x">Value to be converted into string.</param>
		public static string BoolToString(bool x)
		{
			return x ? "1" : "0";
		}

		public static double ParseChar(char c)
		{
			if (int.TryParse(c.ToString(), out int xyz))
			{
				return xyz;
			}
			else
			{
				return Convert.ToInt32(c) - 87;
			}
		}
	}
}
