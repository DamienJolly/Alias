using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class InventoryBadgesComposer : IPacketComposer
	{
		private Habbo habbo;

		public InventoryBadgesComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.InventoryBadgesMessageComposer);
			message.WriteInteger(this.habbo.Badges.GetBadges().Count);
			this.habbo.Badges.GetBadges().ForEach(badge =>
			{
				message.WriteInteger(badge.Slot);
				message.WriteString(badge.Code);
			});

			message.WriteInteger(this.habbo.Badges.GetWearingBadges().Count);
			this.habbo.Badges.GetWearingBadges().ForEach(badge =>
			{
				message.WriteInteger(badge.Slot);
				message.WriteString(badge.Code);
			});
			return message;
		}
	}
}
