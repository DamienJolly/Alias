using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class CatalogUpdatedComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.CatalogUpdatedMessageComposer);
			message.Boolean(false);
			return message;
		}
	}
}
