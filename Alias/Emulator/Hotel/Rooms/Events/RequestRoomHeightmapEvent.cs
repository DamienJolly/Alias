using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Models.Composers;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	class RequestRoomHeightmapEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomEntity user = new RoomEntity()
			{
				Id = session.Player.Id,
				Name = session.Player.Username,
				Motto = session.Player.Motto,
				Look = session.Player.Look,
				Gender = session.Player.Gender,
				Type = RoomEntityType.Player,
				Room = room,
				Position = new UserPosition()
				{
					X = room.Model.Door.X,
					Y = room.Model.Door.Y,
					Rotation = room.Model.Door.Rotation,
					HeadRotation = room.Model.Door.Rotation
				},
				Player = session.Player
			};

			session.Player.CurrentRoom.EntityManager.CreateEntity(user);

			session.Send(new RoomRelativeMapComposer(room));
			session.Send(new RoomHeightMapComposer(room));
			session.Send(new RoomEntryInfoComposer(room, session.Player));
			session.Send(new RoomThicknessComposer(room));

			session.Send(new RoomUsersComposer(room.EntityManager.Users));
			session.Send(new RoomUsersComposer(room.EntityManager.Bots));
			session.Send(new RoomUsersComposer(room.EntityManager.Pets));
			session.Send(new RoomUserStatusComposer(room.EntityManager.Entities));

			session.Send(new RoomFloorItemsComposer(room.ItemManager.FloorItems));
			session.Send(new RoomWallItemsComposer(room.ItemManager.WallItems));

			room.RoomRights.RefreshRights(session.Player);
		}
	}
}
