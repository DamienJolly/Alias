using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomBotSaveSettingsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (room.RoomData.OwnerId != session.Habbo.Id)
			{
				return;
			}

			int botId = message.PopInt();
			RoomEntity bot = room.EntityManager.BotById(botId);
			if (bot == null)
			{
				return;
			}

			int settingId = message.PopInt();

			switch (settingId)
			{
				case 1:
					bot.Look = session.Habbo.Look;
					bot.Gender = session.Habbo.Gender;
					bot.EffectId = session.Habbo.Entity.EffectId;
					room.EntityManager.Send(new RoomUserDataComposer(bot));
					room.EntityManager.Send(new RoomUserEffectComposer(bot));
					break;

				case 2:
					//to-do
					break;

				case 3:
					bot.CanWalk = !bot.CanWalk;
					break;

				case 4:
					if (bot.DanceId != 0)
					{
						bot.DanceId = 0;
					}
					else
					{
						bot.DanceId = Randomness.RandomNumber(1, 4);
					}

					room.EntityManager.Send(new RoomUserDanceComposer(bot));
					break;

				case 5:
					string name = message.PopString();
					if (name.Length > 25)
					{
						return;
					}

					bot.Name = name;
					room.EntityManager.Send(new RoomUsersComposer(bot));
					break;
			}

			RoomEntityDatabase.UpdateBot(bot);
		}
	}
}
