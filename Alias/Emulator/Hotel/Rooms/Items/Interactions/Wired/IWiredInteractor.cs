using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions.Wired
{
	public interface IWiredInteractor
	{
		void Serialize(ServerMessage message);
		void LoadBox(RoomItem item);
		void OnTrigger(RoomUser target = null);
		void OnCycle();
	}
}