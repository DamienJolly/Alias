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
			RoomDatabase.UserRooms(session.Player.Id).ForEach(roomData =>
			{
				//todo: check if room is promoted
				rooms.Add(roomData);
			});

			session.Send(new PromoteOwnRoomsListComposer(rooms));
		}
	}
}
