using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Hotel.Players.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserPickupPetEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomEntity pet = room.EntityManager.PetById(message.PopInt());
			if (pet == null)
			{
				return;
			}

			InventoryPet iPet = new InventoryPet(pet.Id, pet.Name, 0, 0, "");
			RoomEntityDatabase.RemovePet(pet);
			await session.Player.Inventory.UpdatePet(iPet);
			session.Player.CurrentRoom.EntityManager.OnUserLeave(pet);
			session.Send(new AddPetComposer(iPet));
		}
	}
}
