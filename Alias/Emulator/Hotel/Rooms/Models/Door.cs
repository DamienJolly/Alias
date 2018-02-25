using System;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Rooms.Models
{
	public class Door
	{
		public short X
		{
			get; set;
		} = 0;

		public short Y
		{
			get; set;
		} = 0;

		public int Rotation
		{
			get; set;
		} = 0;

		public static Door Parse(string toParse)
		{
			if (toParse.Split(',').Length < 3)
			{
				Logging.Info("Corrupted Door Data!!");
				return new Models.Door();
			}
			try
			{
				Door d = new Door();
				d.X = short.Parse(toParse.Split(',')[0]);
				d.Y = short.Parse(toParse.Split(',')[1]);
				d.Rotation = int.Parse(toParse.Split(',')[2]);
				return d;
			}
			catch (Exception ex)
			{
				Logging.Error("Corrupted Door Data!!", ex, "Models.Door", "Parse");
				return new Door();
			}
		}
	}
}
