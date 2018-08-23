using System;

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

		public Alias()
		{
			Server = new AliasServer();
			Server.Initialize();
		}
	}
}
