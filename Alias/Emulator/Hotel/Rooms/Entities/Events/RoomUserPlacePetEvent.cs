using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserPlacePetEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			InventoryPets pet = session.Habbo.Inventory.GetPet(message.PopInt());
			if (pet == null)
			{
				return;
			}
			int x = message.PopInt();
			int y = message.PopInt();
			
			if (!room.Mapping.Tiles[x, y].IsValidTile(null, true))
			{
				return;
			}

			RoomEntity entity = new RoomEntity
			{
				Id = pet.Id,
				Name = pet.Name,
				Motto = "",
				Look = pet.Type + " " + pet.Race + " " + pet.Colour + " 2 2 4 0 0",
				Gender = pet.Type + "",
				OwnerId = session.Habbo.Id,
				Type = RoomEntityType.Pet,
				Room = session.Habbo.CurrentRoom,
				Position = new UserPosition()
				{
					X = x,
					Y = y,
					//todo: v
					Rotation = session.Habbo.CurrentRoom.Model.Door.Rotation,
					HeadRotation = session.Habbo.CurrentRoom.Model.Door.Rotation
				}
			};
			
			session.Habbo.CurrentRoom.EntityManager.CreateEntity(entity);

			pet.RoomId = room.Id;
			session.Habbo.Inventory.UpdatePet(pet);
			session.Send(new RemovePetComposer(pet));
		}
	}
}
