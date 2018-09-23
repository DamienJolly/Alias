using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Inventory.Composers
{
	public class RemovePetComposer : IPacketComposer
	{
		private InventoryPet _pet;

		public RemovePetComposer(InventoryPet pet)
		{
			_pet = pet;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RemovePetMessageComposer);
			message.WriteInteger(_pet.Id);
			return message;
		}
	}
}
