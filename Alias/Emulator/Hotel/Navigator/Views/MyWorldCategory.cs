using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Views
{
	internal class MyWorldCategory : INavigatorCategory
	{
		public override List<RoomData> Search(string query, Session session, int Limit)
		{
			List<RoomData> rooms = new List<RoomData>();
			switch (base.QueryId)
			{
				case "my":
					{
						foreach (var room in RoomDatabase.UserRooms(session.Habbo.Id))
						{
							if (rooms.Count >= Limit)
							{
								break;
							}

							if (!room.Name.ToLower().Contains(query))
							{
								continue;
							}

							rooms.Add(room);
						}
						break;
					}
			}
			return rooms;
		}
	}
}
