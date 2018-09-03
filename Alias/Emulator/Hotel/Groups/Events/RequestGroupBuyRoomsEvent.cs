using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class RequestGroupBuyRoomsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			List<RoomData> rooms = new List<RoomData>();
			RoomDatabase.UserRooms(session.Habbo.Id).ForEach(Id =>
			{
				if (!Alias.Server.RoomManager.TryGetRoomData(Id, out RoomData roomData))
				{
					if (roomData.Group == null)
					{
						rooms.Add(roomData);
					}
				}
			});
			
			session.Send(new GroupBuyRoomsComposer(rooms.ToList()));
		}
	}
}
