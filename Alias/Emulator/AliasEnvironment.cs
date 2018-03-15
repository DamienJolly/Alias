using System;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Catalog;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Misc.WordFilter;
using Alias.Emulator.Hotel.Moderation;
using Alias.Emulator.Hotel.Navigator;
using Alias.Emulator.Hotel.Permissions;
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
		/// <summary>
		/// Set the server time started.
		/// </summary>
		private static DateTime _started;

		/// <summary>
		/// Gets the time the server was initialized.
		/// </summary>
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
				return Constant.Version;
			}
		}

		/// <summary>
		/// Initialize our application.
		/// </summary>
		public static void Initialize()
		{
			Logging.Alias("Alias Emulator is starting up...", Version);
			Logging.CreateExceptionFile();

			_started = DateTime.Now;

			// Load configiration data.
			Configuration.Initialize();

			// Load that database manager.
			DatabaseClient.Initialize();
			DatabaseClient.TestConnection();

			// Load all our libraries.
			WordFilterManager.Initialize();
			MessageHandler.Initialize();
			AchievementManager.Initialize();
			ModerationManager.Initialize();
			PermissionManager.Initialize();
			ItemManager.Initialize();
			CatalogManager.Initialize();
			RoomModelManager.Initialize();
			RoomManager.Initialize();
			Navigator.Initialize();
			SessionManager.Initialize();
			CommandHandler.Initialize();
			SocketServer.Initialize();

			// Load the task manager.
			TaskManager.Initialize();

			while (true) Logging.ReadLine();

			//ConsoleCommand.Initialize();
		}

		/// <summary>
		/// Current server uptime in string format.
		/// </summary>
		public static string GetUpTime()
		{
			TimeSpan Uptime = DateTime.Now - _started;
			return Uptime.Days + " day(s), " + Uptime.Hours + " hour(s) and " + Uptime.Minutes + " minute(s)";
		}

		/// <summary>
		/// Current server time in unix timestamp format.
		/// </summary>
		public static double GetUnixTimestamp()
		{
			return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
		}

		/// <summary>
		/// Converts a string into a boolean.
		/// </summary>
		/// <param name="x">Value to be converted into bool.</param>
		public static bool ToBool(string x)
		{
			return x.Equals("1");
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
