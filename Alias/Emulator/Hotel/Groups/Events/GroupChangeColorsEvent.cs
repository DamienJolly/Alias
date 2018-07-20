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
			group.Save();

			RoomData room = Alias.Server.RoomManager.RoomData(group.RoomId);
			if (room != null && room.Group != null)
			{
				room.Group = group;
			}

			if (Alias.Server.RoomManager.IsRoomLoaded(group.RoomId))
			{
				Alias.Server.RoomManager.Room(group.RoomId).RefreshGroup();

				//todo: update group furni colours
			}
		}
	}
}
