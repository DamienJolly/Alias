using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Types
{
    class EntityPet : IEntityType
    {
		public override void Serialize(ServerPacket message)
		{
			message.WriteInteger(2);
			message.WriteInteger(int.Parse(Entity.Gender));
			message.WriteInteger(Entity.OwnerId);
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

		public override void OnEntityJoin()
		{

		}

		public override void OnEntityLeave()
		{

		}

		public override void OnCycle()
		{
			
		}
	}
}
