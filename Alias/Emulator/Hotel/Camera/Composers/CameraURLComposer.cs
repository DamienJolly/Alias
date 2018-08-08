using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Camera.Composers
{
	class CameraURLComposer : IPacketComposer
	{
		string URL;

		public CameraURLComposer(string URL)
		{
			this.URL = URL;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CameraURLMessageComposer);
			message.WriteString(this.URL);
			return message;
		}
	}
}
