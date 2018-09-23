using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Hotel.Players.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserPlaceBotEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int botId = message.PopInt();
			if(!session.Player.Inventory.TryGetBot(botId, out InventoryBot bot))
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
				OwnerId = session.Player.Id,
				Type = RoomEntityType.Bot,
				Room = session.Player.CurrentRoom,
				Position = new UserPosition()
				{
					X = x,
					Y = y,
					Rotation = session.Player.CurrentRoom.Model.Door.Rotation,
					HeadRotation = session.Player.CurrentRoom.Model.Door.Rotation
				}
			};

			session.Player.CurrentRoom.EntityManager.CreateEntity(entity);
			RoomEntityDatabase.AddBot(entity);

			bot.RoomId = room.Id;
			await session.Player.Inventory.UpdateBot(bot);
			session.Send(new RemoveBotComposer(bot));
		}
	}
}
