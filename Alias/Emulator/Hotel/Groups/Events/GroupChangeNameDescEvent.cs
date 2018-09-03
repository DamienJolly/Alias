using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupChangeNameDescEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null || group.OwnerId != session.Habbo.Id)
			{
				return;
			}

			string name = message.PopString();
			string description = message.PopString();
			if (name.Length <= 0 || (name == group.Name && description == group.Description))
			{
				return;
			}

			group.Name = name;
			group.Description = description;

			if (!Alias.Server.RoomManager.TryGetRoomData(group.RoomId, out RoomData roomData))
			{
				return;
			}

			if (roomData.Group == null)
			{
				return;
			}

			roomData.Group = group;

			//todo: group fixs
			/*if (Alias.Server.RoomManager.IsRoomLoaded(group.RoomId))
			{
				Alias.Server.RoomManager.Room(group.RoomId).RefreshGroup();
			}*/
		}
	}
}
