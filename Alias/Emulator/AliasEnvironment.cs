using System;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Catalog;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Hotel.Rooms.Users.Chat.Commands;
using Alias.Emulator.Network;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Tasks;
using Alias.Emulator.Utilities;

namespace Alias.Emulator
{
    class AliasEnvironment
    {
		private static string version = "v0.1";
		public static DateTime ServerStarted;
		public static TaskManager tasks;

		public static void Initialize()
		{
			ServerStarted = DateTime.Now;
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
			AchievementManager.Initialize();
			ItemManager.Initialize();
			CatalogManager.Initialize();
			RoomModelManager.Initialize();
			RoomManager.Initialize();
			Navigator.Initialize();
			SessionManager.Initialize();
			CommandHandler.Initialize();
			SocketServer.Initialize();

			tasks = new TaskManager();

			while (true) Logging.ReadLine();
		}

		public static string UpTime()
		{
			TimeSpan Uptime = DateTime.Now - ServerStarted;
			return Uptime.Days + " day(s), " + Uptime.Hours + " hours and " + Uptime.Minutes + " minutes.";
		}

		public static double Time()
		{
			return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
		}

		public static bool ToBool(string x)
		{
			return x.Equals("1");
		}

		public static string BoolToString(bool x)
		{
			return x ? "1" : "0";
		}

		public static double ParseChar(char c)
		{
			int xyz = 0;
			if (int.TryParse(c.ToString(), out xyz))
			{
				return (double)xyz;
			}
			else
			{
				return (double)(Convert.ToInt32(c) - 87);
			}
		}
	}
}
