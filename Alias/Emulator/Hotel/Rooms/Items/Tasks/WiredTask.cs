using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Rooms.Items.Tasks
{
    class WiredTask
	{
		public static void Start(List<RoomItem> items)
		{
			items.ForEach(item =>
			{
				try
				{
					item.GetWiredInteractor().OnCycle();
				}
				catch { }
			});
		}
	}
}
