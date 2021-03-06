using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Rooms.Items.Tasks
{
    class ItemTask
    {
		public static void Start(List<RoomItem> items)
		{
			items.ForEach(item =>
			{
				try
				{
					item.GetInteractor().OnCycle(item);
				}
				catch { }
			});
		}
	}
}
