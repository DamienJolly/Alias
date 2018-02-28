using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	public class RequestHeightmapEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new FurnitureAliasesComposer());
		}
	}
}
