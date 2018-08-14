using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions.Wired
{
	interface IWiredInteractor
	{
		void Serialize(ServerPacket message);
		void LoadBox(RoomItem item);
		void OnTrigger(RoomEntity target = null);
		void OnCycle();
	}
}
