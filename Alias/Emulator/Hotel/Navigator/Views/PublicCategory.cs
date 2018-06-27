using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Views
{
	class PublicCategory : INavigatorCategory
	{
		private List<RoomData> Rooms;

		public override void Init()
		{
			this.Rooms = NavigatorDatabase.ReadPublicRooms(base.ExtraId);
		}

		public override List<RoomData> Search(string query, Session session, int Limit)
		{
			if (string.IsNullOrEmpty(query))
			{
				return this.Rooms.Take(Limit).ToList();
			}
			return new List<RoomData>();
		}
	}
}
