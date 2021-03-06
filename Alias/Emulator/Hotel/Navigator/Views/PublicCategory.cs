using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Views
{
	internal class PublicCategory : INavigatorCategory
	{
		public override List<RoomData> Search(string query, Session session, int Limit)
		{
			List<RoomData> rooms = new List<RoomData>();
			switch (base.QueryId)
			{
				case "public":
					{
						foreach (var room in Alias.Server.NavigatorManager.PublicRooms)
						{
							if (rooms.Count >= Limit)
							{
								break;
							}

							if (room.Name.ToLower().Contains(query))
							{
								rooms.Add(room);
							}
						}
						break;
					}
				default:
					break;
			}
			return rooms;
		}
	}
}
