using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class RequestFriendsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			//
		}
	}
}
