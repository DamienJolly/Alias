using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class DiscountComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.DiscountMessageComposer);
			message.Int(100); //Most you can get.
			message.Int(6);
			message.Int(1);
			message.Int(1);
			message.Int(2); //Count
			{
				message.Int(40);
				message.Int(99);
			}
			return message;
		}
	}
}
