using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Types
{
    class EntityPet : IEntityType
    {
		public void Serialize(ServerPacket message, RoomEntity pet)
		{
			message.WriteInteger(2);
			message.WriteInteger(int.Parse(pet.Gender));
			message.WriteInteger(pet.OwnerId);
			message.WriteString("Damien"); // Owner name
			message.WriteInteger(1); // rarity
			message.WriteBoolean(false); //sadle
			message.WriteBoolean(false); //riding horse
			message.WriteBoolean(false); //can breed
			message.WriteBoolean(false); //fully grown
			message.WriteBoolean(false); //can revive
			message.WriteBoolean(false); //public breed?
			message.WriteInteger(0); //level
			message.WriteString("");
		}

		public void OnEntityJoin(RoomEntity pet)
		{

		}

		public void OnEntityLeave(RoomEntity pet)
		{

		}

		public void OnCycle(RoomEntity pet)
		{
			
		}
	}
}
