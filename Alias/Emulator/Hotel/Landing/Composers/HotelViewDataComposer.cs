using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class HotelViewDataComposer : MessageComposer
	{
		private string data;
		private string key;

		public HotelViewDataComposer(string data, string key)
		{
			this.data = data;
			this.key = key;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.HotelViewDataMessageComposer);
			message.String(this.data);
			message.String(this.key);
			return message;
		}
	}
}
