using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class RequestGroupInfoEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int guildId = message.PopInt();
			bool newWindow = message.PopBoolean();

			Group group = Alias.Server.GroupManager.GetGroup(guildId);
			if (group != null)
			{
				session.Send(new GroupInfoComposer(group, session.Habbo, newWindow));
			}
		}
	}
}
