using Alias.Emulator.Hotel.Rooms.Entities.Chat;
using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Entities.Types
{
    class EntityGenericBot : IEntityType
    {
		private int ActionTimer = 0;
		private int SpeechTimer = 0;

		public void Serialize(ServerPacket message, RoomEntity player)
		{
			message.WriteInteger(4);
			message.WriteString(player.Gender.ToLower()); // ?
			message.WriteInteger(player.OwnerId);
			message.WriteString("Damien"); // Owner name
			message.WriteInteger(5);
			{
				message.WriteInteger(5);//Action Count
				message.WriteShort(1);//Copy looks
				message.WriteShort(2);//Setup speech
				message.WriteShort(3);//Relax
				message.WriteShort(4);//Dance
				message.WriteShort(5);//Change name
			}
		}

		public void OnEntityJoin(RoomEntity player)
		{

		}

		public void OnEntityLeave(RoomEntity player)
		{

		}

		public void OnCycle(RoomEntity player)
		{
			if (SpeechTimer <= 0)
			{
				player.OnChat("testing", 1, ChatType.CHAT);
				SpeechTimer = 20;
			}
			else
			{
				SpeechTimer--;
			}

			if (ActionTimer <= 0)
			{
				RoomTile tile = player.Room.Mapping.RandomWalkableTile;
				if (tile != null)
				{
					player.TargetPosition = new UserPosition()
					{
						X = tile.Position.X,
						Y = tile.Position.Y
					};
					player.Path = player.Room.PathFinder.Path(player);
				}
				ActionTimer = Randomness.RandomNumber(5, 20);
			}
			else
			{
				ActionTimer--;
			}
		}
	}
}
