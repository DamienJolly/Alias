using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class RoomUsersGroupBadgesComposer : IPacketComposer
	{
		private Dictionary<int, string> groupBadges;

		public RoomUsersGroupBadgesComposer(Dictionary<int, string> groupBadges)
		{
			this.groupBadges = groupBadges;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUsersGroupBadgesMessageComposer);
			message.WriteInteger(this.groupBadges.Count);
			foreach (var badge in this.groupBadges)
			{
				message.WriteInteger(badge.Key);
				message.WriteString(badge.Value);
			}
			return message;
		}
	}
}
