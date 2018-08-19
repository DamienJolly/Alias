using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserPickupBotEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomEntity bot = room.EntityManager.BotById(message.PopInt());
			if (bot == null)
			{
				return;
			}

			InventoryBots iBot = new InventoryBots
			{
				Id = bot.Id,
				Name = bot.Name,
				Motto = bot.Motto,
				Look = bot.Look,
				Gender = bot.Gender,
				DanceId = bot.DanceId,
				EffectId = bot.EffectId,
				CanWalk = bot.CanWalk,
				RoomId = 0
			};

			session.Habbo.Inventory.UpdateBot(iBot);
			session.Habbo.CurrentRoom.EntityManager.OnUserLeave(bot);
			session.Send(new AddBotComposer(iBot));
		}
	}
}
