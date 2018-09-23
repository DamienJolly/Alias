using Alias.Emulator.Hotel.Players.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
	class RequestUserDataEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new UserDataComposer(session.Player));
			session.Send(new UserPerksComposer(session.Player));
		}
	}
}
