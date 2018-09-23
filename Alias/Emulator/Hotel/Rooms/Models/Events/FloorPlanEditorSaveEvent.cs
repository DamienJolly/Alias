using System.Collections.Generic;
using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Rooms.Composers;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Models.Events
{
	class FloorPlanEditorSaveEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.HasPermission("acc_floorplan_editor"))
			{
				return;
			}

			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (room.RoomData.OwnerId == session.Player.Id)
			{
				string map = message.PopString().ToLower().TrimEnd();
				if (map.Length == 0)
				{
					return;
				}
				
				string[] data = map.Split('\r');
				if (data.Length > 64)
				{
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
					Alias.Server.RoomManager.RoomModelManager.AddModel(layout);
				}
				room.Model = layout;
				Alias.Server.RoomManager.RoomModelManager.SaveModel(layout);
				room.RoomData.ModelName = room.Model.Name;
				room.RoomData.Settings.FloorSize = floorSize;
				room.RoomData.Settings.WallHeight = wallSize;
				room.Disposing = true;
				//todo: fix
				List<RoomEntity> habbos = room.EntityManager.Entities;
				room = Alias.Server.RoomManager.LoadRoom(room.Id);
				foreach (RoomEntity habbo in habbos)
				{
					habbo.Player.Session.Send(new ForwardToRoomComposer(room.Id));
				}
			}
		}
	}
}
