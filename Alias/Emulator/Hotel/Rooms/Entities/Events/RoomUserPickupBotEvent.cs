using Alias.Emulator.Hotel.Players.Inventory;
using Alias.Emulator.Hotel.Players.Inventory.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserPickupBotEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomEntity bot = room.EntityManager.BotById(message.PopInt());
			if (bot == null)
			{
				return;
			}

			InventoryBot iBot = new InventoryBot(bot.Id, bot.Name, bot.Motto, bot.Look, bot.Gender, bot.DanceId, bot.EffectId, bot.CanWalk);
			RoomEntityDatabase.RemoveBot(bot);
			await session.Player.Inventory.UpdateBot(iBot);
			session.Player.CurrentRoom.EntityManager.OnUserLeave(bot);
			session.Send(new AddBotComposer(iBot));
		}
	}
}
