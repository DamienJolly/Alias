using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserPickupPetEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomEntity pet = room.EntityManager.PetById(message.PopInt());
			if (pet == null)
			{
				return;
			}

			InventoryPets iPet = new InventoryPets
			{
				Id = pet.Id,
				Name = pet.Name,
				Type = 0,
				Race = 0,
				Colour = "",
				RoomId = 0
			};

			RoomEntityDatabase.RemovePet(pet);
			session.Habbo.Inventory.UpdatePet(iPet);
			session.Habbo.CurrentRoom.EntityManager.OnUserLeave(pet);
			session.Send(new AddPetComposer(iPet));
		}
	}
}
