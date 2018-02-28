using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Currency.Composers
{
	public class UserPointsComposer : IMessageComposer
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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.UserPointsMessageComposer);
			result.Int(this.currentAmount);
			result.Int(this.amountAdded);
			result.Int(this.type);
			return result;
		}
	}
}
