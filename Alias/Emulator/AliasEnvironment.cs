using System;
using Alias.Emulator.Network;
using Alias.Emulator.Utilities;

namespace Alias.Emulator
{
    class AliasEnvironment
    {
		private static string version = "v0.1";

		public static void Initialize()
		{
			Logging.Alias("Alias Emulator is starting up...", version);
			Logging.CreateExceptionFile();
			Configuration.Initialize();

			SocketServer.Initialize();
			while (true) Logging.ReadLine();
		}

		public static double Time()
		{
			return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
		}
	}
}
