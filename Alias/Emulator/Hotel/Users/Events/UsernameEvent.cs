using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class UsernameEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			//todo: daily login achivements ext
		}
	}
}
