using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Promotions.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Promotions.Events
{
	class RequestPromotionRoomsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			List<RoomData> rooms = new List<RoomData>();
			RoomDatabase.UserRooms(session.Habbo.Id).ForEach(Id =>
			{
				//todo: check if room is promoted
				if (Alias.Server.RoomManager.TryGetRoomData(Id, out RoomData roomData))
				{
					rooms.Add(roomData);
				}
			});

			session.Send(new PromoteOwnRoomsListComposer(rooms));
		}
	}
}
