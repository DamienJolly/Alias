using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	class SavePreferOldChatEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			bool oldChat = message.PopBoolean();

			session.Habbo.Settings.OldChat = oldChat;
		}
	}
}
