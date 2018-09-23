using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Navigator
{
	internal class NavigatorSearch
	{
		public int Id { get; set; }
		public string Page { get; set; }
		public string Code { get; set; }

		public NavigatorSearch(DbDataReader reader)
		{
			Id = reader.ReadData<int>("id");
			Page = reader.ReadData<string>("page");
			Code = reader.ReadData<string>("code");
		}

		public NavigatorSearch(string page, string code)
		{
			Id = -1;
			Page = page;
			Code = code;
		}
	}
}
