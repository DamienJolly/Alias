using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class HotelViewDataComposer : IPacketComposer
	{
		private string data;
		private string key;

		public HotelViewDataComposer(string data, string key)
		{
			this.data = data;
			this.key = key;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.HotelViewDataMessageComposer);
			message.WriteString(this.data);
			message.WriteString(this.key);
			return message;
		}
	}
}
