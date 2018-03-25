using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
    class RequestUserProfileEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int userId = message.Integer();
			if (userId <= 0)
			{
				//todo: open group chat
				return;
			}
			
			session.Send(new UserProfileComposer(SessionManager.HabboById(userId), session));
		}
	}
}
