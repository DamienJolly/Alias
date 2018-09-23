using System.Collections.Generic;
using Alias.Emulator.Hotel.Players.Badges;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	public class UserBadgesComposer : IPacketComposer
	{
		private List<BadgeDefinition> _badges;
		private int _id;

		public UserBadgesComposer(List<BadgeDefinition> badges, int id)
		{
			_badges = badges;
			_id = id;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserBadgesMessageComposer);
			message.WriteInteger(_id);
			message.WriteInteger(_badges.Count);
			_badges.ForEach(badge =>
			{
				message.WriteInteger(badge.Slot);
				message.WriteString(badge.Code);
			});
			return message;
		}
	}
}
