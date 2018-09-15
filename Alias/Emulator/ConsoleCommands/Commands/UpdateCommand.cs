using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.ConsoleCommands.Commands
{
	class UpdateCommand : IConsoleCommand
	{
		public string Name => "Update";

		public string Description => "Updates various aspects of the hotel.";

		public string Arguments => string.Empty;

		public bool Handle(string[] args)
		{
			if (args.Length == 0)
			{
				Logging.Info("You must inculde something to update, e.g. :update catalog");
				return false;
			}

			string type = args[0];
			switch (type.ToLower())
			{
				case "cata":
				case "catalog":
				case "catalogue":
					{
						Alias.Server.CatalogManager.Initialize();
						Alias.Server.SocketServer.SessionManager.Send(new CatalogUpdatedComposer());
						break;
					}

				case "items":
				case "furni":
				case "furniture":
					{
						Alias.Server.ItemManager.Initialize();
						break;
					}

				case "ach":
				case "achievements":
					{
						Alias.Server.AchievementManager.Initialize().Wait();
						break;
					}

				case "navi":
				case "navigator":
					{
						Alias.Server.NavigatorManager.Initialize();
						break;
					}

				case "models":
					{
						Alias.Server.RoomManager.Initialize();
						break;
					}

				default:
					Logging.Info("'" + type + "' is not a valid item to be updated.");
					return false;
			}

			Logging.Info("'" + type + "' was successfully updated.");
			return true;
		}
	}
}
