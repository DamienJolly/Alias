using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomPaintComposer : IPacketComposer
	{
		private string type;
		private string val;

		public RoomPaintComposer(string t, string v)
		{
			this.type = t;
			this.val = v;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomPaintMessageComposer);
			message.WriteString(this.type);
			message.WriteString(this.val);
			return message;
		}
	}
}
