using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupDeleteEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null || group.OwnerId != session.Player.Id)
			{
				return;
			}

			Alias.Server.GroupManager.DeleteGroup(group);

			if (!Alias.Server.RoomManager.TryGetRoomData(group.RoomId, out RoomData roomData))
			{
				return;
			}

			roomData.Group = null;

			if (session.Player.CurrentRoom != null)
			{
				session.Player.CurrentRoom.RoomData.Group = null;
				session.Player.CurrentRoom.EntityManager.Send(new RemoveGroupFromRoomComposer(group.Id));
			}
		}
	}
}
