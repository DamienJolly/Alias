using System;
using System.IO;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Users.Handshake.Composers;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Users.Handshake
{
	public class Handshake
	{
		public static void OnLogin(string sso, Session session)
		{
			try
			{
				if (!string.IsNullOrEmpty(sso) && HandshakeDatabase.SSOExists(sso))
				{
					session.AssignHabbo(HandshakeDatabase.BuildHabbo(sso));
					if (HandshakeDatabase.IsBanned(session.Habbo().Id))
					{
						//todo: banned message
						session.Disconnect();
						return;
					}

					session.Habbo().Init();
					session.Send(new SecureLoginOKComposer());
					//session.Send(new UserHomeRoomComposer()); //todo:
					//session.Send(new UserEffectsListComposer()); //todo:
					//session.Send(new UserClothesComposer()); //todo:
					//session.Send(new NewUserIdentityComposer()); //todo:
					session.Send(new UserPermissionsComposer(session.Habbo().Rank)); //todo: user rank
					session.Send(new SessionRightsComposer());
					session.Send(new SomeConnectionComposer());
					session.Send(new DebugConsoleComposer());
					//session.Send(new UserAchievementScoreComposer()); //todo:
					session.Send(new UnknownComposer4());
					session.Send(new UnknownComposer5());
					//session.Send(new BuildersClubExpiredComposer()); //todo:
					//session.Send(new CfhTopicsMessageComposer()); //todo:
					//session.Send(new FavoriteRoomsCountComposer()); //todo:
					//session.Send(new GameCenterGameListComposer()); //todo:
					//session.Send(new GameCenterAccountInfoComposer()); //todo:
					//session.Send(new UserClubComposer()); //todo:

					//TODO Modtool

					//session.Send(new NavigatorMetaDataComposer());
					//session.Send(new NavigatorLiftedRoomsComposer());
					//session.Send(new NavigatorCollapsedCategoriesComposer());
					//session.Send(new NavigatorSavedSearchesComposer());
					//session.Send(new NavigatorEventCategoriesComposer());
					//session.Send(new NavigatorSettingsComposer();
					//session.Send(new InventoryRefreshComposer());
					//session.Send(new ForumsTestComposer());
					//session.Send(new InventoryAchievementsComposer());
					//session.Send(new AchievementListComposer());

					if(File.Exists(@".\welcome.alias"))
					{
						string message = File.ReadAllText(@".\welcome.alias");
						if (!string.IsNullOrEmpty(message))
						{
							session.Send(new GenericAlertComposer(message.Replace("\r\n", "\n"), session));
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
				Logging.Error("An Error occured on the Login process. Disconnecting the User for safety...", ex, "Handshake", "OnLogin");
				session.Disconnect();
			}
		}
	}
}
