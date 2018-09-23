using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	public class AddPetComposer : IPacketComposer
	{
		InventoryPet _pet;

		public AddPetComposer(InventoryPet pet)
		{
			_pet = pet;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AddPetMessageComposer);
			message.WriteInteger(_pet.Id);
			message.WriteString(_pet.Name);
			message.WriteInteger(_pet.Type);
			message.WriteInteger(_pet.Race);
			message.WriteString(_pet.Colour);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteBoolean(false);
			return message;
		}
	}
}
