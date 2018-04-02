using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Views
{
	public class NormalCategory : INavigatorCategory
	{
		public override void Init()
		{

		}

		public override List<RoomData> Search(string query, Session session, int Limit)
		{
			List<RoomData> result = new List<RoomData>();
			switch (base.Id)
			{
				case "popular":
					{
						if (!string.IsNullOrEmpty(query))
						{
							RoomDatabase.AllRooms().ForEach(Id =>
							{
								result.Add(Alias.Server.RoomManager.RoomData(Id));
							});
						}
						else
						{
							Alias.Server.RoomManager.ReadLoadedRooms().ForEach(room =>
							{
								if (room.RoomData.UsersNow > 0)
								{
									result.Add(room.RoomData);
								}
							});
						}
						return result.Where(room => room.Name.ToLower().Contains(query)).ToList();
					}
				default:
					{
						Alias.Server.RoomManager.ReadLoadedRooms().ForEach(room =>
						{
							if (room.RoomData.UsersNow > 0 && room.RoomData.Category == base.ExtraId)
							{
								result.Add(room.RoomData);
							}
						});
						return result.Where(room => room.Name.ToLower().Contains(query)).ToList();
					}
			}
		}
	}
}
