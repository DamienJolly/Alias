using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Navigator
{
    internal class NavigatorSettings
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public bool ShowSearches { get; set; }
		public int UnknownInt { get; set; }

		public NavigatorSettings(DbDataReader reader)
		{
			X = reader.ReadData<int>("x");
			Y = reader.ReadData<int>("y");
			Height = reader.ReadData<int>("height");
			Width = reader.ReadData<int>("width");
			ShowSearches = reader.ReadBool("show_searches");
			UnknownInt = reader.ReadData<int>("unknown_int");
		}
	}
}
