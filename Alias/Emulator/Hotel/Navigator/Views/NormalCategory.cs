using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Views
{
	internal class NormalCategory : INavigatorCategory
	{
		public override List<RoomData> Search(string query, Session session, int Limit)
		{
			List<RoomData> rooms = new List<RoomData>();
			switch (base.QueryId)
			{
				case "popular":
					{
						foreach (var room in Alias.Server.RoomManager.LoadedRooms.Values)
						{
							if (rooms.Count >= Limit)
							{
								break;
							}

							if (room.RoomData.UsersNow <= 0 || !room.RoomData.Name.ToLower().Contains(query))
							{
								continue;
							}

							rooms.Add(room.RoomData);
						}
						break;
					}
				default:
					{
						foreach (var room in Alias.Server.RoomManager.LoadedRooms.Values)
						{
							if (rooms.Count >= Limit)
							{
								break;
							}

							if (room.RoomData.UsersNow <= 0 || !room.RoomData.Name.ToLower().Contains(query) || room.RoomData.Category != base.Id)
							{
								continue;
							}

							rooms.Add(room.RoomData);
						}
					}
					break;
			}
			return rooms;
		}
	}
}
