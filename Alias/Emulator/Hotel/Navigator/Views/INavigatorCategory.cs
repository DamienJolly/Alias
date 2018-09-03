using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Navigator.Views
{
	abstract class INavigatorCategory
	{
		public string Id
		{
			get; set;
		}

		public string Title
		{
			get; set;
		}

		public bool ShowButtons
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

		public string Type
		{
			get; set;
		}

		public int OrderId
		{
			get; set;
		}

		public int ExtraId
		{
			get; set;
		}

		public int MinRank
		{
			get; set;
		}

		public abstract void Init();

		public abstract List<RoomData> Search(string query, Session session, int Limit);
	}
}
