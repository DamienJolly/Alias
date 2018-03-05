using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationRequestUserInfoEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_user_info"))
			{
				return;
			}

			int userId = message.Integer();
			if (userId <= 0)
			{
				return;
			}

			Habbo target = SessionManager.HabboById(userId);
			if (target != null)
			{
				session.Send(new ModerationUserInfoComposer(target));
			}
		}
	}
}
