using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	public class RemoveFloorItemComposer : IPacketComposer
	{
		private RoomItem item;

		public RemoveFloorItemComposer(RoomItem item)
		{
			this.item = item;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RemoveFloorItemMessageComposer);
			message.WriteString(this.item.Id + "");
			message.WriteBoolean(false);
			message.WriteInteger(this.item.Owner);
			message.WriteInteger(0);
			return message;
		}
	}
}
