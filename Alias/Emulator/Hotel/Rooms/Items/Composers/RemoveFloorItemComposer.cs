using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Composers
{
	public class RemoveFloorItemComposer : IMessageComposer
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
