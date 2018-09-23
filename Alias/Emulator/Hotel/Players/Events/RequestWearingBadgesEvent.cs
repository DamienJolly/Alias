using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Hotel.Players.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
	class RequestWearingBadgesEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int userId = message.PopInt();
			Player player = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(userId);

			if (player.Badges != null)
			{
				session.Send(new UserBadgesComposer(player.Badges.GetWearingBadges, userId));
			}
		}
	}
}
