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

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.InventoryBadgesMessageComposer);
			message.Int(this.habbo.Badges.GetBadges().Count);
			this.habbo.Badges.GetBadges().ForEach(badge =>
			{
				message.Int(badge.Slot);
				message.String(badge.Code);
			});

			message.Int(this.habbo.Badges.GetWearingBadges().Count);
			this.habbo.Badges.GetWearingBadges().ForEach(badge =>
			{
				message.Int(badge.Slot);
				message.String(badge.Code);
			});
			return message;
		}
	}
}
