using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Camera.Composers
{
	class CameraPriceComposer : IPacketComposer
	{
		private int credits;
		private int points;
		private int publishCost;

		public CameraPriceComposer(int credits, int points, int publishCost)
		{
			this.credits = credits;
			this.points = points;
			this.publishCost = publishCost;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.CameraPriceMessageComposer);
			message.WriteInteger(this.credits);
			message.WriteInteger(this.points);
			message.WriteInteger(this.publishCost);
			return message;
		}
	}
}
