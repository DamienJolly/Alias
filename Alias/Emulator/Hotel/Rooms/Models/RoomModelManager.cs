using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Models
{
	sealed class RoomModelManager
	{
		private List<RoomModel> _models;

		public RoomModelManager()
		{
			this._models = new List<RoomModel>();
		}

		public void Initialize()
		{
			this._models = RoomModelDatabase.Models();
		}

		public bool ModelExists(string name)
		{
			return this._models.Where(m => m.Name.Equals(name)).Count() > 0;
		}

		public RoomModel GetModel(string name)
		{
			if (ModelExists(name))
			{
				return this._models.Where(m => m.Name.Equals(name)).First();
			}
			Logging.Info("Couldn't find Model with Name " + name + ". Returning an empty Model!");
			return new RoomModel();
		}

		public void AddModel(RoomModel model)
		{
			this._models.Add(model);
		}

		public void SaveModel(RoomModel model)
		{
			RoomModelDatabase.SaveModel(model);
		}
	}
}
