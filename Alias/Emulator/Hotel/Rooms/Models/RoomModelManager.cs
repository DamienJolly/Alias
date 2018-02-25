using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Models
{
	public class RoomModelManager
	{
		private static List<RoomModel> Models;

		public static void Initialize()
		{
			RoomModelManager.Models = RoomModelDatabase.Models();
		}

		public static bool ModelExists(string name)
		{
			return RoomModelManager.Models.Where(m => m.Name.Equals(name)).Count() > 0;
		}

		public static RoomModel GetModel(string name)
		{
			if (ModelExists(name))
			{
				return RoomModelManager.Models.Where(m => m.Name.Equals(name)).First();
			}
			Logging.Info("Couldn't find Model with Name " + name + ". Returning an empty Model!");
			return new RoomModel();
		}
	}
}
