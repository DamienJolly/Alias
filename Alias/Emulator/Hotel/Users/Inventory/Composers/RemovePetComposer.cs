using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class RemovePetComposer : IPacketComposer
	{
		InventoryPets pet;

		public RemovePetComposer(InventoryPets pet)
		{
			this.pet = pet;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RemovePetMessageComposer);
			message.WriteInteger(pet.Id);
			return message;
		}
	}
}
