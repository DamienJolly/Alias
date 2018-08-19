using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class AddPetComposer : IPacketComposer
	{
		InventoryPets pet;

		public AddPetComposer(InventoryPets pet)
		{
			this.pet = pet;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AddPetMessageComposer);
			message.WriteInteger(pet.Id);
			message.WriteString(pet.Name);
			message.WriteInteger(pet.Type);
			message.WriteInteger(pet.Race);
			message.WriteString(pet.Colour);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteBoolean(false);
			return message;
		}
	}
}
