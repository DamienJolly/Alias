using System.IO;
using Alias.Emulator.Hotel.Achievements.Composers;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	class SecureLoginEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string sso = message.PopString();
			if (TryLoadHabbo(sso, out Habbo habbo))
			{
				session.AssignHabbo(habbo);
				habbo.Init();
				session.Send(new SecureLoginOKComposer());
				session.Send(new UserHomeRoomComposer(habbo.HomeRoom));
				session.Send(new UserEffectsListComposer()); //todo:
				session.Send(new UserClothesComposer()); //todo:
				session.Send(new NewUserIdentityComposer());
				session.Send(new UserPermissionsComposer(habbo));
				session.Send(new SessionRightsComposer());
				session.Send(new SomeConnectionComposer());
				session.Send(new DebugConsoleComposer());
				session.Send(new UserAchievementScoreComposer(habbo.AchievementScore));
				session.Send(new UnknownComposer4());
				session.Send(new UnknownComposer5());
				//session.Send(new BuildersClubExpiredComposer()); //todo:
				session.Send(new ModerationTopicsComposer());
				//session.Send(new FavoriteRoomsCountComposer()); //todo:

				if (habbo.HasPermission("acc_modtool"))
				{
					session.Send(new ModerationToolComposer(habbo, Alias.Server.ModerationManager.GetTickets));
				}

				session.Send(new InventoryRefreshComposer());
				session.Send(new AchievementListComposer(habbo));

				if (File.Exists(@".\welcome.alias"))
				{
					string welcome = File.ReadAllText(@".\welcome.alias");
					if (!string.IsNullOrEmpty(welcome))
					{
						session.Send(new GenericAlertComposer(welcome.Replace("\r\n", "\n")));
					}
				}
			}
			else
			{
				session.Disconnect();
			}
		}

		private bool TryLoadHabbo(string sso, out Habbo habbo)
		{
			habbo = null;
			if (!string.IsNullOrEmpty(sso))
			{
				if (!UserDatabase.ReadUserIdBySSO(sso, out int userId))
				{
					return false;
				}

				if (UserDatabase.IsBanned(userId))
				{
					return false;
				}

				if (UserDatabase.ReadHabboData(userId, out habbo))
				{
					return true;
				}
			}
			return false;
		}
	}
}
