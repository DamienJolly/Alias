using Alias.Emulator.Hotel.Achievements;
using Alias.Emulator.Hotel.Catalog;
using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Hotel.Items;
using Alias.Emulator.Hotel.Rooms.Models;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Chat.Commands.Users
{
	public class UpdateCommand : ICommand
	{
		public override string Name
		{
			get
			{
				return "update";
			}
		}

		public override string Description
		{
			get
			{
				return "Updates verious.";
			}
		}

		public override string Arguments
		{
			get
			{
				return string.Empty;
			}
		}

		public override bool Handle(string[] args, Session session)
		{
			if (args.Length == 0)
			{
				session.Habbo.Notification("You must inculde something to update, e.g. :update catalog");
				return true;
			}

			string type = args[0];
			switch (type.ToLower())
			{
				case "cata":
				case "catalog":
				case "catalogue":
					{
						CatalogManager.Reload();
						SessionManager.Send(new CatalogUpdatedComposer());
						break;
					}

				case "items":
				case "furni":
				case "furniture":
					{
						ItemManager.Reload();
						break;
					}

				case "ach":
				case "achievements":
					{
						AchievementManager.Reload();
						break;
					}

				case "navi":
				case "navigator":
					{
						Navigator.Navigator.Reload();
						break;
					}

				case "models":
					{
						RoomModelManager.Reload();
						break;
					}

				default:
					session.Habbo.Notification("'" + type + "' is not a valid item to be updated.");
					return true;
			}

			session.Habbo.Notification("'" + type + "' was successfully updated.");

			return true;
		}
	}
}
