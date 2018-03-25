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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomPaintMessageComposer);
			result.String(this.type);
			result.String(this.val);
			return result;
		}
	}
}
