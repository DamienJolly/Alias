using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	class HotelViewEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Player.CurrentRoom != null)
			{
				session.Player.CurrentRoom.EntityManager.OnUserLeave(session.Player.Entity);
			}
		}
	}
}
