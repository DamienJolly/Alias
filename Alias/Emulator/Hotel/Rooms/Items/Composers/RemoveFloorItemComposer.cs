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

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.RemoveFloorItemMessageComposer);
			message.String(this.item.Id + "");
			message.Boolean(false);
			message.Int(this.item.Owner);
			message.Int(0);
			return message;
		}
	}
}
