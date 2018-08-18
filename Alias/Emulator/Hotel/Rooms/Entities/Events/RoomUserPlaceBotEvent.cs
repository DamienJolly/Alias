using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserPlaceBotEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			InventoryBots bot = session.Habbo.Inventory.GetBot(message.PopInt());
			if (bot == null)
			{
				return;
			}

			int x = message.PopInt();
			int y = message.PopInt();
			
			if (!room.Mapping.Tiles[x, y].IsValidTile(null, true))
			{
				return;
			}

			RoomEntity entity = new RoomEntity()
			{
				Id = bot.Id,
				Name = bot.Name,
				Motto = bot.Motto,
				Look = bot.Look,
				Gender = bot.Gender,
				OwnerId = session.Habbo.Id,
				Type = RoomEntityType.Bot,
				Room = session.Habbo.CurrentRoom,
				Position = new UserPosition()
				{
					X = x,
					Y = y,
					//todo: v
					Rotation = session.Habbo.CurrentRoom.Model.Door.Rotation,
					HeadRotation = session.Habbo.CurrentRoom.Model.Door.Rotation
				}
			};

			session.Habbo.CurrentRoom.EntityManager.CreateEntity(entity);

			bot.RoomId = room.Id;
			session.Habbo.Inventory.UpdateBot(bot);
			session.Habbo.CurrentRoom.EntityManager.AddBot(entity);

			// remove bot
		}
	}
}
