using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Models.Composers;
using Alias.Emulator.Hotel.Users;
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
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomEntity user = new RoomEntity()
			{
				Id = session.Habbo.Id,
				Name = session.Habbo.Username,
				Motto = session.Habbo.Motto,
				Look = session.Habbo.Look,
				Gender = session.Habbo.Gender,
				Type = RoomEntityType.Player,
				Room = room,
				Position = new UserPosition()
				{
					X = room.Model.Door.X,
					Y = room.Model.Door.Y,
					Rotation = room.Model.Door.Rotation,
					HeadRotation = room.Model.Door.Rotation
				},
				Habbo = session.Habbo
			};

			session.Habbo.CurrentRoom.EntityManager.CreateEntity(user);

			session.Send(new RoomRelativeMapComposer(room));
			session.Send(new RoomHeightMapComposer(room));
			session.Send(new RoomEntryInfoComposer(room, session.Habbo));
			session.Send(new RoomThicknessComposer(room));

			session.Send(new RoomUsersComposer(room.EntityManager.Users));
			session.Send(new RoomUsersComposer(room.EntityManager.Bots));
			session.Send(new RoomUsersComposer(room.EntityManager.Pets));
			session.Send(new RoomUserStatusComposer(room.EntityManager.Entities));

			session.Send(new RoomFloorItemsComposer(room.ItemManager.FloorItems));
			session.Send(new RoomWallItemsComposer(room.ItemManager.WallItems));

			room.RoomRights.RefreshRights(session.Habbo);
		}
	}
}
