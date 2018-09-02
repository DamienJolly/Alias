using System;
using System.IO;
using System.Reflection;

namespace Alias.Emulator
{
    class Alias
    {
		public static AliasServer Server
		{
			get; set;
		}

		public static DateTime ServerStarted
		{
			get; private set;
		} = DateTime.Now;

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

		public static string ProductionVersion
		{
			get
			{
				return "PRODUCTION-201802201205-141713395";
			}
		}

		public static string GetFileFromDictionary(string path)
		{
			return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);
		}

		public Alias()
		{
			Server = new AliasServer();
			Server.Initialize();
		}
	}
}
