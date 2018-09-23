using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupChangeSettingsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null || group.OwnerId != session.Player.Id)
			{
				return;
			}
			
			int state = message.PopInt();
			if (state < 0 || state > 2)
			{
				return;
			}

			bool rights = message.PopInt() == 0 ? true : false;

			if (state == (int)group.State && rights == group.Rights)
			{
				return;
			}
			
			group.State = (GroupState)state;
			group.Rights = rights;

			if (!Alias.Server.RoomManager.TryGetRoomData(group.RoomId, out RoomData roomData))
			{
				return;
			}

			if (roomData.Group == null)
			{
				return;
			}

			roomData.Group = group;

			if (session.Player.CurrentRoom != null)
			{
				session.Player.CurrentRoom.UpdateGroup(group);
			}
		}
	}
}
