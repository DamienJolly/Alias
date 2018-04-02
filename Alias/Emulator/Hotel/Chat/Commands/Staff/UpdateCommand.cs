using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Chat.Commands.Staff
{
	class UpdateCommand : IChatCommand
	{
		public string Name => "update";

		public string Description => "Updates various aspects of the hotel.";

		public bool IsAsynchronous => false;

		public void Handle(Session session, string[] args)
		{
			if (args.Length == 0)
			{
				session.Habbo.Notification("You must inculde something to update, e.g. :update catalog");
				return;
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
						Alias.Server.AchievementManager.Initialize();
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
					session.Habbo.Notification("'" + type + "' is not a valid item to be updated.");
					return;
			}

			session.Habbo.Notification("'" + type + "' was successfully updated.");
		}
	}
}
