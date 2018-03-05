using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationRequestUserChatlogEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_user_logs"))
			{
				return;
			}

			int userId = message.Integer();
			if (userId <= 0)
			{
				return;
			}

			//todo: chatlogs
		}
	}
}
