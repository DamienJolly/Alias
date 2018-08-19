using Alias.Emulator.Hotel.Rooms.Entities.Chat;
using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Hotel.Rooms.Mapping;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Entities.Types
{
    class EntityGenericBot : IEntityType
    {
		private int ActionTimer = 0;
		private int SpeechTimer = 0;

		public void Serialize(ServerPacket message, RoomEntity bot)
		{
			message.WriteInteger(4);
			message.WriteString(bot.Gender.ToLower()); // ?
			message.WriteInteger(bot.OwnerId);
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

		public void OnEntityJoin(RoomEntity bot)
		{
			bot.Room.EntityManager.Send(new RoomUserDanceComposer(bot));
			bot.Room.EntityManager.Send(new RoomUserEffectComposer(bot));
		}

		public void OnEntityLeave(RoomEntity bot)
		{

		}

		public void OnCycle(RoomEntity bot)
		{
			if (SpeechTimer <= 0)
			{
				bot.OnChat("testing", 1, ChatType.CHAT);
				SpeechTimer = 20;
			}
			else
			{
				SpeechTimer--;
			}

			if (ActionTimer <= 0)
			{
				RoomTile tile = bot.Room.Mapping.RandomWalkableTile;
				if (tile != null)
				{
					bot.TargetPosition = new UserPosition()
					{
						X = tile.Position.X,
						Y = tile.Position.Y
					};
					bot.Path = bot.Room.PathFinder.Path(bot);
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
