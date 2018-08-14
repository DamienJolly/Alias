using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	class HotelViewEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Habbo.CurrentRoom != null)
			{
				session.Habbo.CurrentRoom.EntityManager.OnUserLeave(session.Habbo.Entity);
			}
		}
	}
}
