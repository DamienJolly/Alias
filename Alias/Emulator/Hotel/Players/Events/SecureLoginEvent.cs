using System.IO;
using Alias.Emulator.Hotel.Achievements.Composers;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Players.Composers;
using Alias.Emulator.Hotel.Players.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
	class SecureLoginEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			string sso = message.PopString();
			if (!string.IsNullOrEmpty(sso))
			{
				Player player = await Alias.Server.PlayerManager.ReadPlayerBySSOAsync(sso);
				if (player != null)
				{
					session.Player = player;
					await session.Player.Initialize(session);
					session.Send(new SecureLoginOKComposer());
					session.Send(new UserHomeRoomComposer(player.HomeRoom));
					session.Send(new UserEffectsListComposer()); //todo:
					session.Send(new UserClothesComposer()); //todo:
					session.Send(new NewUserIdentityComposer());
					session.Send(new UserPermissionsComposer(player));
					session.Send(new SessionRightsComposer());
					session.Send(new SomeConnectionComposer());
					session.Send(new DebugConsoleComposer());
					session.Send(new UserAchievementScoreComposer(player.AchievementScore));
					session.Send(new UnknownComposer4());
					session.Send(new UnknownComposer5());
					//session.Send(new BuildersClubExpiredComposer()); //todo:
					session.Send(new ModerationTopicsComposer());
					//session.Send(new FavoriteRoomsCountComposer()); //todo:

					if (player.HasPermission("acc_modtool"))
					{
						session.Send(new ModerationToolComposer(player, Alias.Server.ModerationManager.GetTickets));
					}

					session.Send(new InventoryRefreshComposer());
					session.Send(new AchievementListComposer(player));

					if (File.Exists(@".\welcome.alias"))
					{
						string welcome = File.ReadAllText(@".\welcome.alias");
						if (!string.IsNullOrEmpty(welcome))
						{
							session.Send(new GenericAlertComposer(welcome.Replace("\r\n", "\n")));
						}
					}
					return;
				}
			}

			session.Disconnect();
		}
	}
}
