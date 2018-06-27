using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Inventory.Composers
{
	public class InventoryPetsComposer : IPacketComposer
	{
		private List<InventoryPets> pets;

		public InventoryPetsComposer(List<InventoryPets> pets)
		{
			this.pets = pets;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.InventoryPetsMessageComposer);
			message.WriteInteger(1);
			message.WriteInteger(1);
			message.WriteInteger(this.pets.Count);
			this.pets.ForEach(pet =>
			{
				message.WriteInteger(pet.Id);
				message.WriteString(pet.Name);
				message.WriteInteger(pet.Type);
				message.WriteInteger(pet.Race);
				message.WriteString(pet.Colour);
				message.WriteInteger(0);
				message.WriteInteger(0);
				message.WriteInteger(0);
			});
			return message;
		}
	}
}
