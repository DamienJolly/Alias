using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class InventoryBadgesComposer : MessageComposer
	{
		private Habbo habbo;

		public InventoryBadgesComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.InventoryBadgesMessageComposer);
			message.Int(this.habbo.GetBadgeComponent().GetBadges().Count);
			this.habbo.GetBadgeComponent().GetBadges().ForEach(badge =>
			{
				message.Int(badge.Slot);
				message.String(badge.Code);
			});

			message.Int(this.habbo.GetBadgeComponent().GetWearingBadges().Count);
			this.habbo.GetBadgeComponent().GetWearingBadges().ForEach(badge =>
			{
				message.Int(badge.Slot);
				message.String(badge.Code);
			});
			return message;
		}
	}
}
