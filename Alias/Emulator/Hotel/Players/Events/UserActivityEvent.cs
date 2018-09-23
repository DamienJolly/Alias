using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
	class UserActivityEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string type = message.PopString();
			string value = message.PopString();

			//todo: do some shit here or w.e
		}
	}
}
