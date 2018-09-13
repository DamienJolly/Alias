using System;
using System.IO;
using Alias.Emulator.Hotel.Achievements.Composers;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Navigator.Composers;
using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Hotel.Users.Handshake.Composers;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Users.Handshake
{
	class Handshake
	{
		public static void OnLogin(string sso, Session session)
		{
			try
			{
				if (!string.IsNullOrEmpty(sso) && HandshakeDatabase.SSOExists(sso))
				{
					session.AssignHabbo(HandshakeDatabase.BuildHabbo(sso));
					if (session.Habbo == null || HandshakeDatabase.IsBanned(session.Habbo.Id))
					{
						session.Disconnect();
						return;
					}

					session.Habbo.Init();
					session.Send(new SecureLoginOKComposer());
					session.Send(new UserHomeRoomComposer(session.Habbo.HomeRoom));
					session.Send(new UserEffectsListComposer()); //todo:
					session.Send(new UserClothesComposer()); //todo:
					session.Send(new NewUserIdentityComposer());
					session.Send(new UserPermissionsComposer(session.Habbo.Rank));
					session.Send(new SessionRightsComposer());
					session.Send(new SomeConnectionComposer());
					session.Send(new DebugConsoleComposer());
					session.Send(new UserAchievementScoreComposer(session.Habbo.AchievementScore));
					session.Send(new UnknownComposer4());
					session.Send(new UnknownComposer5());
					//session.Send(new BuildersClubExpiredComposer()); //todo:
					session.Send(new ModerationTopicsComposer());
					//session.Send(new FavoriteRoomsCountComposer()); //todo:

					if (session.Habbo.HasPermission("acc_modtool"))
					{
						session.Send(new ModerationToolComposer(session.Habbo, Alias.Server.ModerationManager.GetTickets));
					}

					session.Send(new InventoryRefreshComposer());
					session.Send(new AchievementListComposer(session.Habbo));

					if(File.Exists(@".\welcome.alias"))
					{
						string message = File.ReadAllText(@".\welcome.alias");
						if (!string.IsNullOrEmpty(message))
						{
							session.Send(new GenericAlertComposer(message.Replace("\r\n", "\n")));
						}
					}
				}
				else
				{
					session.Disconnect();
				}
			}
			catch (Exception ex)
			{
				Logging.Error("An Error occured on the Login process. Disconnecting the User for safety...", ex);
				session.Disconnect();
			}
		}
	}
}
