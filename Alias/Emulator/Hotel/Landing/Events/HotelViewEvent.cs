using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	public class HotelViewEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (session.Habbo.CurrentRoom != null)
			{
				session.Habbo.CurrentRoom.UserManager.OnUserLeave(session);
			}
		}
	}
}
