using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationSanctionMuteEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_user_mute"))
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
				string text = message.String();
				double duration = 60 * 60;

				// todo: muting
				target.Habbo.Muted = true;
				target.Send(new ModerationIssueHandledComposer(message.String()));
			}
		}
	}
}
