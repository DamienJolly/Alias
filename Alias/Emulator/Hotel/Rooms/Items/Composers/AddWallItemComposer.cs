using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	class AddWallItemComposer : IPacketComposer
	{
		private RoomItem item;

		public AddWallItemComposer(RoomItem item)
		{
			this.item = item;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AddWallItemMessageComposer);
			message.WriteString(item.Id + "");
			message.WriteInteger(item.ItemData.SpriteId);
			message.WriteString(item.Position.WallPosition);
			message.WriteString(item.Mode + "");
			message.WriteInteger(-1);
			message.WriteInteger(item.ItemData.Modes > 1 ? 1 : 0);
			message.WriteInteger(item.Owner);
			message.WriteString("Damien"); //todo: owner name
			return message;
		}
	}
}
