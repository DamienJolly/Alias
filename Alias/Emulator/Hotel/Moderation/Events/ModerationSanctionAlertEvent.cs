using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationSanctionAlertEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_user_alert"))
			{
				return;
			}
			
			int userId = message.Integer();
			if (userId <= 0)
			{
				return;
			}

			Session target = SessionManager.SessionById(message.Integer());
			if (target != null)
			{
				target.Send(new ModerationIssueHandledComposer(message.String()));
			}
		}
	}
}
