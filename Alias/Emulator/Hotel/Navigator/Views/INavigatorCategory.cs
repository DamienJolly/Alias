using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Views
{
	abstract class INavigatorCategory
	{
		public int Id
		{
			get; set;
		}

		public string QueryId
		{
			get; set;
		}
		
		public string Title
		{
			get; set;
		}

		public bool ShowCollapsed
		{
			get; set;
		}

		public bool ShowThumbnail
		{
			get; set;
		}

		public int OrderId
		{
			get; set;
		}

		public int MinRank
		{
			get; set;
		}

		public abstract List<RoomData> Search(string query, Session session, int Limit);
	}
}
