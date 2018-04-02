using System.Collections.Generic;
using Alias.Emulator.Hotel.Users.Badges;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserBadgesComposer : IPacketComposer
	{
		private List<BadgeDefinition> badges;
		private int id;

		public UserBadgesComposer(List<BadgeDefinition> badges, int id)
		{
			this.badges = badges;
			this.id = id;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserBadgesMessageComposer);
			message.WriteInteger(this.id);
			message.WriteInteger(badges.Count);
			badges.ForEach(badge =>
			{
				message.WriteInteger(badge.Slot);
				message.WriteString(badge.Code);
			});
			return message;
		}
	}
}
