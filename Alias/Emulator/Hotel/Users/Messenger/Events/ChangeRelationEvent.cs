using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger.Events
{
	public class ChangeRelationEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int userId = message.Integer();
			if (!session.Habbo.Messenger.IsFriend(userId))
			{
				return;
			}

			int type = message.Integer();
			if (type < 0 || type > 3)
			{
				return;
			}

			session.Habbo.Messenger.SetRelation(userId, type);
		}
	}
}
