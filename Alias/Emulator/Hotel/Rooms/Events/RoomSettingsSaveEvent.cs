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
			if (!Alias.Server.RoomManager.TryGetRoomData(roomId, out RoomData roomData))
			{
				return;
			}

			if (roomData.OwnerId != session.Habbo.Id)
			{
				return;
			}

			string name = message.PopString();
			if (name.Length <= 0)
			{
				session.Send(new RoomEditSettingsErrorComposer(roomData.Id, RoomEditSettingsErrorComposer.ROOM_NAME_MISSING));
			}
			else
			{
				if (name.Length > 60)
				{
					name = name.Substring(0, 60);
				}
				roomData.Name = name;
			}

			roomData.Description = message.PopString();
			
			RoomDoorState state = (RoomDoorState)message.PopInt();

			string password = message.PopString();
			if (roomData.DoorState == RoomDoorState.PASSWORD && password.Length <= 0)
			{
				session.Send(new RoomEditSettingsErrorComposer(roomData.Id, RoomEditSettingsErrorComposer.PASSWORD_REQUIRED));
			}
			else
			{
				roomData.DoorState = state;
				roomData.Password = password;
			}

			roomData.MaxUsers = message.PopInt();
			if (roomData.MaxUsers < 0)
			{
				roomData.MaxUsers = 10;
			}
			else if (roomData.MaxUsers > 50)
			{
				roomData.MaxUsers = 50;
			}

			roomData.Category = message.PopInt();

			List<string> tags = new List<string>();
			for (int i = 0; i < message.PopInt() && i < 2; i++)
			{
				string tag = message.PopString();

				if (tag.Length > 15)
				{
					session.Send(new RoomEditSettingsErrorComposer(roomData.Id, RoomEditSettingsErrorComposer.TAGS_TOO_LONG));
					continue;
				}

				tags.Add(tag);
			}

			roomData.Tags = tags;
			roomData.TradeState = (RoomTradeState)message.PopInt();
			roomData.Settings.AllowPets = message.PopBoolean();
			roomData.Settings.AllowPetsEat = message.PopBoolean();
			roomData.Settings.RoomBlocking = message.PopBoolean();
			roomData.Settings.HideWalls = message.PopBoolean();

			roomData.Settings.WallHeight = message.PopInt();
			if (roomData.Settings.WallHeight < -2 || roomData.Settings.WallHeight > 1)
			{
				roomData.Settings.WallHeight = 0;
			}

			roomData.Settings.FloorSize = message.PopInt();
			if (roomData.Settings.FloorSize < -2 || roomData.Settings.FloorSize > 1)
			{
				roomData.Settings.FloorSize = 0;
			}

			roomData.Settings.WhoMutes = message.PopInt();
			if (roomData.Settings.WhoMutes < 0 || roomData.Settings.WhoMutes > 1)
			{
				roomData.Settings.WhoMutes = 0;
			}

			roomData.Settings.WhoKicks = message.PopInt();
			if (roomData.Settings.WhoKicks < 0 || roomData.Settings.WhoKicks > 1)
			{
				roomData.Settings.WhoKicks = 0;
			}

			roomData.Settings.WhoBans = message.PopInt();
			if (roomData.Settings.WhoBans < 0 || roomData.Settings.WhoBans > 1)
			{
				roomData.Settings.WhoBans = 0;
			}

			roomData.Settings.ChatMode = message.PopInt();
			if (roomData.Settings.ChatMode < 0 || roomData.Settings.ChatMode > 1)
			{
				roomData.Settings.ChatMode = 0;
			}

			roomData.Settings.ChatSize = message.PopInt();
			if (roomData.Settings.ChatSize < 0 || roomData.Settings.ChatSize > 2)
			{
				roomData.Settings.ChatSize = 0;
			}

			roomData.Settings.ChatSpeed = message.PopInt();
			if (roomData.Settings.ChatSpeed < 0 || roomData.Settings.ChatSpeed > 2)
			{
				roomData.Settings.ChatSpeed = 0;
			}

			roomData.Settings.ChatDistance = message.PopInt();
			if (roomData.Settings.ChatDistance < 0)
			{
				roomData.Settings.ChatDistance = 1;
			}
			else if (roomData.Settings.ChatDistance > 99)
			{
				roomData.Settings.ChatDistance = 100;
			}

			roomData.Settings.ChatFlood = message.PopInt();
			if (roomData.Settings.ChatFlood < 0 || roomData.Settings.ChatFlood > 2)
			{
				roomData.Settings.ChatFlood = 0;
			}
			
			if (Alias.Server.RoomManager.TryGetRoom(roomData.Id, out Room room))
			{
				room.EntityManager.Send(new RoomThicknessComposer(room));
				room.EntityManager.Send(new RoomChatSettingsComposer(roomData));
				room.EntityManager.Send(new RoomSettingsUpdatedComposer(roomData.Id));
			}

			session.Send(new RoomSettingsSavedComposer(roomData.Id));
		}
	}
}
