using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class RequestGroupManageEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Group group = Alias.Server.GroupManager.GetGroup(message.PopInt());

			if (group != null)
			{
				session.Send(new GroupManageComposer(group));
			}
		}
	}
}
