using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Hotel.Players.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserPlacePetEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int petId = message.PopInt();
			if(!session.Player.Inventory.TryGetPet(petId, out InventoryPet pet))
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
				OwnerId = session.Player.Id,
				Type = RoomEntityType.Pet,
				Room = session.Player.CurrentRoom,
				Position = new UserPosition()
				{
					X = x,
					Y = y,
					Rotation = session.Player.CurrentRoom.Model.Door.Rotation,
					HeadRotation = session.Player.CurrentRoom.Model.Door.Rotation
				}
			};
			
			session.Player.CurrentRoom.EntityManager.CreateEntity(entity);
			RoomEntityDatabase.AddPet(entity);

			pet.RoomId = room.Id;
			await session.Player.Inventory.UpdatePet(pet);
			session.Send(new RemovePetComposer(pet));
		}
	}
}
