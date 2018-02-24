using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorMetaDataComposer : MessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.NavigatorMetaDataMessageComposer);
			message.Int(4);
			message.String("official_view");
			message.Int(0);
			message.String("hotel_view");
			message.Int(0);
			message.String("roomads_view");
			message.Int(0);
			message.String("myworld_view");
			message.Int(0);
			return message;
		}
	}
}
