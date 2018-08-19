using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Composers
{
	class PetInformationComposer : IPacketComposer
	{
		private RoomEntity pet;

		public PetInformationComposer(RoomEntity pet)
		{
			this.pet = pet;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.PetInformationMessageComposer);
			message.WriteInteger(pet.Id);
			message.WriteString(pet.Name);
			message.WriteInteger(0); //level
			message.WriteInteger(20); //max level
			message.WriteInteger(0); //exp
			message.WriteInteger(20); //xp goal
			message.WriteInteger(80); //energy
			message.WriteInteger(100); //max energy
			message.WriteInteger(70); //happyness
			message.WriteInteger(100); //max happyness
			message.WriteInteger(10); //respects
			message.WriteInteger(pet.OwnerId);
			message.WriteInteger(10); //days old
			message.WriteString("Damien"); //owner name
			message.WriteInteger(0); //rarity
			message.WriteBoolean(false); //sadle
			message.WriteBoolean(false); //riding horse
			message.WriteInteger(0); //?
			message.WriteInteger(0); //can ride
			message.WriteBoolean(false); //can breed
			message.WriteBoolean(false); //fully grown
			message.WriteBoolean(false); //can revive
			message.WriteInteger(0); //rarity
			message.WriteInteger(0); //??
			message.WriteInteger(0); //??
			message.WriteInteger(0); //??
			message.WriteBoolean(false); //public breed?
			return message;
		}
	}
}
