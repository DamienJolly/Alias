using Alias.Emulator.Hotel.Rooms.Entities.Chat;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserTalkEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (session.Player != null && session.Player.CurrentRoom != null)
			{
				string text = message.PopString();
				RoomEntity target = null;
				if (GetChatType(message.Header) == ChatType.WHISPER)
				{
					target = session.Player.CurrentRoom.EntityManager.UserByName(text.Split(' ')[0]);
					if (target == null || target.Player.CurrentRoom == null)
					{
						return;
					}
					
					text = text.Substring(text.Split(' ')[0].Length + 1);
				}

				session.Player.Entity.OnChat(text, message.PopInt(), GetChatType(message.Header), target);
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
