using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Views
{
	internal class EventsCategory : INavigatorCategory
	{
		public override List<RoomData> Search(string query, Session session, int Limit)
		{
			List<RoomData> rooms = new List<RoomData>();
			switch (base.QueryId)
			{
				default:
					break;
			}
			return rooms;
		}
	}
}
