using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class CatalogModeComposer : IMessageComposer
	{
		private int mode;

		public CatalogModeComposer(int mode)
		{
			this.mode = mode;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.CatalogModeMessageComposer);
			result.Int(this.mode);
			return result;
		}
	}
}
