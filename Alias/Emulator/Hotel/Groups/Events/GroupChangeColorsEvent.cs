using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupChangeColorsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null || group.OwnerId != session.Habbo.Id)
			{
				return;
			}

			int colourOne = message.PopInt();
			int colourTwo = message.PopInt();
			if (colourOne == group.ColourOne && colourTwo == group.ColourTwo)
			{
				return;
			}

			group.ColourOne = colourOne;
			group.ColourTwo = colourTwo;

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

				//todo: update group furni colours
			}*/
		}
	}
}
