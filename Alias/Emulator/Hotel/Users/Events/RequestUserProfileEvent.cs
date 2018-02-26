using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
    class RequestUserProfileEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int userId = message.Integer();

			if (userId <= 0)
			{
				//todo: open group chat
				return;
			}

			Habbo targetData = SessionManager.SessionById(userId).Habbo();

			if (targetData == null)
			{
				session.Habbo().Notification("An error occured whilst finding that user's profile.");
				return;
			}

			session.Send(new UserProfileComposer(targetData, session));
		}
	}
}
