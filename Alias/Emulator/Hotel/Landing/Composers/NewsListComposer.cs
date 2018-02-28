using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class NewsListComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.NewsListMessageComposer);
			message.Int(0);
			return message;
		}
	}
}
