using System;
using Alias.Emulator.Database;
using Alias.Emulator.Network;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Sessions;
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

			try
			{
				DatabaseClient.Initialize();
				using (DatabaseClient client = DatabaseClient.Instance())
				{
					string srv = client.String("SELECT @@version;");
					Logging.Info("Current SQL Version: " + srv);
				}
			}
			catch (Exception ex)
			{
				Logging.Error("Failed Connection to SQL Server", ex, "AliasEnvironment", "Initialize");
				Logging.Info("Press any key to exit.");
				Logging.ReadLine();
				Environment.Exit(0);
			}
			
			MessageHandler.Initialize();
			SessionManager.Initialize();
			SocketServer.Initialize();
			while (true) Logging.ReadLine();
		}

		public static double Time()
		{
			return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
		}
	}
}
