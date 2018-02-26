using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class UsernameEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			//todo: daily login achivements ext
		}
	}
}
