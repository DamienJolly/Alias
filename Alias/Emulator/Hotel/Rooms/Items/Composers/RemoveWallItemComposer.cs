using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	class RemoveWallItemComposer : IPacketComposer
	{
		private RoomItem item;

		public RemoveWallItemComposer(RoomItem item)
		{
			this.item = item;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RemoveWallItemMessageComposer);
			message.WriteString(this.item.Id + "");
			message.WriteInteger(this.item.Owner);
			return message;
		}
	}
}
