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

		public override void Serialize(ServerPacket message)
		{
			message.WriteInteger(4);
			message.WriteString(Entity.Gender.ToLower()); // ?
			message.WriteInteger(Entity.OwnerId);
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

		public override void OnEntityJoin()
		{
			SpeechTimer = 20;
			ActionTimer = Randomness.RandomNumber(5, 20);
			
			CurrentRoom.EntityManager.Send(new RoomUserDanceComposer(Entity));
			CurrentRoom.EntityManager.Send(new RoomUserEffectComposer(Entity));
		}

		public override void OnEntityLeave()
		{

		}

		public override void OnCycle()
		{
			if (SpeechTimer <= 0)
			{
				Entity.OnChat("testing", 1, ChatType.CHAT);
				SpeechTimer = 20;
			}
			else
			{
				SpeechTimer--;
			}

			if (Entity.CanWalk)
			{
				if (ActionTimer <= 0)
				{
					RoomTile tile = CurrentRoom.Mapping.RandomWalkableTile;
					if (tile != null)
					{
						Entity.TargetPosition = new UserPosition()
						{
							X = tile.Position.X,
							Y = tile.Position.Y
						};
						Entity.Path = CurrentRoom.PathFinder.Path(Entity);
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
}
