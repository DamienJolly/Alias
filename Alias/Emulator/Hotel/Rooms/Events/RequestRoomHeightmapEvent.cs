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
			if (session.Habbo.CurrentRoom == null)
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
				Room = session.Habbo.CurrentRoom,
				Position = new UserPosition()
				{
					X = session.Habbo.CurrentRoom.Model.Door.X,
					Y = session.Habbo.CurrentRoom.Model.Door.Y,
					Rotation = session.Habbo.CurrentRoom.Model.Door.Rotation,
					HeadRotation = session.Habbo.CurrentRoom.Model.Door.Rotation
				},
				Habbo = session.Habbo
			};

			session.Habbo.CurrentRoom.EntityManager.CreateEntity(user);

			session.Send(new RoomRelativeMapComposer(session.Habbo.CurrentRoom));
			session.Send(new RoomHeightMapComposer(session.Habbo.CurrentRoom));
			session.Send(new RoomEntryInfoComposer(session.Habbo.CurrentRoom, session.Habbo));
			session.Send(new RoomThicknessComposer(session.Habbo.CurrentRoom));
			session.Send(new RoomUsersComposer(session.Habbo.CurrentRoom.EntityManager.Entities));
			session.Send(new RoomUserStatusComposer(session.Habbo.CurrentRoom.EntityManager.Entities));
			session.Send(new RoomFloorItemsComposer(session.Habbo.CurrentRoom.ItemManager.Items));
			//session.Send(new ItemsComposer()); //todo: wall items

			session.Habbo.CurrentRoom.RoomRights.RefreshRights(session.Habbo);
		}
	}
}
