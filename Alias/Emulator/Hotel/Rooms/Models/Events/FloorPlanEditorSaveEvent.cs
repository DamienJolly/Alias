using System.Collections.Generic;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.Models.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Models.Events
{
	public class FloorPlanEditorSaveEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_floorplan_editor"))
			{
				return;
			}

			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (room.RoomData.OwnerId == session.Habbo.Id)
			{
				string map = message.PopString().ToLower().TrimEnd();
				if (map.Length == 0)
				{
					return;
				}
				
				string[] data = map.Split('\r');
				if (data.Length > 64)
				{
					session.Send(new GenericAlertComposer("Incorrect Floorplan! Maximum size of 64 x 64", session));
					return;
				}

				if (data.Length > 64 || data[0].Length > 64)
				{
					return;
				}

				int lastLineLength = 0;
				for (int i = 0; i < data.Length; i++)
				{
					if (lastLineLength == 0)
					{
						lastLineLength = data[i].Length;
						continue;
					}

					if (lastLineLength != data[i].Length)
					{
						session.Send(new GenericAlertComposer("Incorrect Floorplan! Maximum size of 64 x 64", session));
						return;
					}
				}
				
				int doorX = message.PopInt();
				int doorY = message.PopInt();
				int doorRotation = message.PopInt();
				int wallSize = message.PopInt();
				int floorSize = message.PopInt();
				//int wallHeight = message.PopInt();

				if (doorX < 0 || doorY < 0)
				{
					return;
				}

				RoomModel layout = room.Model;
				if (layout.IsCustom)
				{
					layout.Door.X = (short)doorX;
					layout.Door.Y = (short)doorY;
					layout.Door.Rotation = doorRotation;
					layout.Map = map;
				}
				else
				{
					layout = new RoomModel()
					{
						Name = "model_bc_" + room.Id,
						Map  = map,
						Door = new Door()
						{
							X        = (short)doorX,
							Y        = (short)doorY,
							Rotation = doorRotation
						},
						IsCustom = true
					};
					Alias.Server.RoomManager.GetRoomModelManager().AddModel(layout);
				}
				room.Model = layout;
				Alias.Server.RoomManager.GetRoomModelManager().SaveModel(layout);
				room.RoomData.ModelName = room.Model.Name;
				room.RoomData.Settings.FloorSize = floorSize;
				room.RoomData.Settings.WallHeight = wallSize;
				room.Unload();
				List<RoomUser> habbos = room.UserManager.Users;
				room = Alias.Server.RoomManager.LoadRoom(room.Id);
				foreach (RoomUser habbo in habbos)
				{
					habbo.Habbo.Session.Send(new ForwardToRoomComposer(room.Id));
				}
			}
		}
	}
}
