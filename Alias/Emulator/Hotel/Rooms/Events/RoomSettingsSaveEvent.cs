using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Events
{
	class RoomSettingsSaveEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int roomId = message.PopInt();

			Room room = Alias.Server.RoomManager.Room(roomId);
			if (room == null || room.RoomData.OwnerId != session.Habbo.Id)
			{
				return;
			}

			string name = message.PopString();
			if (name.Length <= 0)
			{
				session.Send(new RoomEditSettingsErrorComposer(room.Id, RoomEditSettingsErrorComposer.ROOM_NAME_MISSING));
			}
			else
			{
				if (name.Length > 60)
				{
					name = name.Substring(0, 60);
				}
				room.RoomData.Name = name;
			}

			room.RoomData.Description = message.PopString();
			
			RoomDoorState state = (RoomDoorState)message.PopInt();

			string password = message.PopString();
			if (room.RoomData.DoorState == RoomDoorState.PASSWORD && password.Length <= 0)
			{
				session.Send(new RoomEditSettingsErrorComposer(room.Id, RoomEditSettingsErrorComposer.PASSWORD_REQUIRED));
			}
			else
			{
				room.RoomData.DoorState = state;
				room.RoomData.Password = password;
			}

			room.RoomData.MaxUsers = message.PopInt();
			if (room.RoomData.MaxUsers < 0)
			{
				room.RoomData.MaxUsers = 10;
			}
			else if (room.RoomData.MaxUsers > 50)
			{
				room.RoomData.MaxUsers = 50;
			}

			room.RoomData.Category = message.PopInt();

			List<string> tags = new List<string>();
			for (int i = 0; i < message.PopInt() && i < 2; i++)
			{
				string tag = message.PopString();

				if (tag.Length > 15)
				{
					session.Send(new RoomEditSettingsErrorComposer(room.Id, RoomEditSettingsErrorComposer.TAGS_TOO_LONG));
					continue;
				}

				tags.Add(tag);
			}
			
			room.RoomData.Tags = tags;
			room.RoomData.TradeState = (RoomTradeState)message.PopInt();
			room.RoomData.Settings.AllowPets = message.PopBoolean();
			room.RoomData.Settings.AllowPetsEat = message.PopBoolean();
			room.RoomData.Settings.RoomBlocking = message.PopBoolean();
			room.RoomData.Settings.HideWalls = message.PopBoolean();

			room.RoomData.Settings.WallHeight = message.PopInt();
			if (room.RoomData.Settings.WallHeight < -2 || room.RoomData.Settings.WallHeight > 1)
			{
				room.RoomData.Settings.WallHeight = 0;
			}

			room.RoomData.Settings.FloorSize = message.PopInt();
			if (room.RoomData.Settings.FloorSize < -2 || room.RoomData.Settings.FloorSize > 1)
			{
				room.RoomData.Settings.FloorSize = 0;
			}

			room.RoomData.Settings.WhoMutes = message.PopInt();
			if (room.RoomData.Settings.WhoMutes < 0 || room.RoomData.Settings.WhoMutes > 1)
			{
				room.RoomData.Settings.WhoMutes = 0;
			}

			room.RoomData.Settings.WhoKicks = message.PopInt();
			if (room.RoomData.Settings.WhoKicks < 0 || room.RoomData.Settings.WhoKicks > 1)
			{
				room.RoomData.Settings.WhoKicks = 0;
			}

			room.RoomData.Settings.WhoBans = message.PopInt();
			if (room.RoomData.Settings.WhoBans < 0 || room.RoomData.Settings.WhoBans > 1)
			{
				room.RoomData.Settings.WhoBans = 0;
			}

			room.RoomData.Settings.ChatMode = message.PopInt();
			if (room.RoomData.Settings.ChatMode < 0 || room.RoomData.Settings.ChatMode > 1)
			{
				room.RoomData.Settings.ChatMode = 0;
			}

			room.RoomData.Settings.ChatSize = message.PopInt();
			if (room.RoomData.Settings.ChatSize < 0 || room.RoomData.Settings.ChatSize > 2)
			{
				room.RoomData.Settings.ChatSize = 0;
			}

			room.RoomData.Settings.ChatSpeed = message.PopInt();
			if (room.RoomData.Settings.ChatSpeed < 0 || room.RoomData.Settings.ChatSpeed > 2)
			{
				room.RoomData.Settings.ChatSpeed = 0;
			}

			room.RoomData.Settings.ChatDistance = message.PopInt();
			if (room.RoomData.Settings.ChatDistance < 0)
			{
				room.RoomData.Settings.ChatDistance = 1;
			}
			else if (room.RoomData.Settings.ChatDistance > 99)
			{
				room.RoomData.Settings.ChatDistance = 100;
			}

			room.RoomData.Settings.ChatFlood = message.PopInt();
			if (room.RoomData.Settings.ChatFlood < 0 || room.RoomData.Settings.ChatFlood > 2)
			{
				room.RoomData.Settings.ChatFlood = 0;
			}

			room.EntityManager.Send(new RoomThicknessComposer(room));
			room.EntityManager.Send(new RoomChatSettingsComposer(room.RoomData));
			room.EntityManager.Send(new RoomSettingsUpdatedComposer(room.Id));
			session.Send(new RoomSettingsSavedComposer(room.Id));
		}
	}
}
