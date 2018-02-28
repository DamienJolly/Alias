using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class AlertLimitedSoldOutComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.AlertLimitedSoldOutMessageComposer);
		}
	}
}
