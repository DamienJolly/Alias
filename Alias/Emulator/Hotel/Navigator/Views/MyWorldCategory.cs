using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Views
{
	public class MyWorldCategory : INavigatorCategory
	{
		public override void Init()
		{
		}

		public override List<RoomData> Search(string query, Session session, int Limit)
		{
			List<RoomData> result = new List<RoomData>();
			switch (base.Id)
			{
				case "my":
					{
						RoomDatabase.UserRooms(session.Habbo.Id).ForEach(Id =>
						{
							result.Add(Alias.Server.RoomManager.RoomData(Id));
						});
						return result.Where(room => room.Name.ToLower().Contains(query)).ToList();
					}
				default:
					return result;
			}
		}
	}
}
