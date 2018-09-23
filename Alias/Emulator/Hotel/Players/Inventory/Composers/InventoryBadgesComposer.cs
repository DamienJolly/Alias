using Alias.Emulator.Hotel.Players.Badges;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	class InventoryBadgesComposer : IPacketComposer
	{
		private readonly Player _player;

		public InventoryBadgesComposer(Player player)
		{
			_player = player;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.InventoryBadgesMessageComposer);
			message.WriteInteger(_player.Badges.Badges.Count);
			foreach (BadgeDefinition badge in _player.Badges.Badges.Values)
			{
				message.WriteInteger(badge.Slot);
				message.WriteString(badge.Code);
			};

			message.WriteInteger(_player.Badges.GetWearingBadges.Count);
			foreach (BadgeDefinition badge in _player.Badges.GetWearingBadges)
			{
				message.WriteInteger(badge.Slot);
				message.WriteString(badge.Code);
			};
			return message;
		}
	}
}
