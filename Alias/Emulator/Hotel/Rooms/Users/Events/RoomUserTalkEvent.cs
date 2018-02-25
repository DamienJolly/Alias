using Alias.Emulator.Hotel.Rooms.Users.Chat;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Users.Events
{
	public class RoomUserTalkEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (session.Habbo() != null && session.Habbo().CurrentRoom != null)
			{
				RoomUser usr = session.Habbo().CurrentRoom.UserManager.UserBySession(session);
				usr.OnChat(message.String(), message.Integer(), GetChatType(message.Id));
			}
		}

		private ChatType GetChatType(int packet)
		{
			ChatType type = ChatType.CHAT;
			if (packet == Incoming.RoomUserWhisperMessageEvent)
			{
				type = ChatType.WHISPER;
			}
			if (packet == Incoming.RoomUserShoutMessageEvent)
			{
				type = ChatType.SHOUT;
			}
			return type;
		}
	}
}
