using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Currency.Composers
{
	public class UserPointsComposer : IPacketComposer
	{
		private int currentAmount;
		private int amountAdded;
		private int type;

		public UserPointsComposer(int currentAmount, int amountAdded, int type)
		{
			this.currentAmount = currentAmount;
			this.amountAdded = amountAdded;
			this.type = type;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserPointsMessageComposer);
			message.WriteInteger(this.currentAmount);
			message.WriteInteger(this.amountAdded);
			message.WriteInteger(this.type);
			return message;
		}
	}
}
