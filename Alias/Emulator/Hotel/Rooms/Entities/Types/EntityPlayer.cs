using Alias.Emulator.Hotel.Rooms.Trading;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Types
{
    class EntityPlayer : IEntityType
    {
		public void Serialize(ServerPacket message, RoomEntity player)
		{

		}

		public void OnEntityJoin(RoomEntity player)
		{
			player.Habbo.Entity = player;
		}

		public void OnEntityLeave(RoomEntity player)
		{
			player.Habbo.CurrentRoom = null;
			player.Habbo.Entity = null;

			RoomTrade trade = player.Room.RoomTrading.GetActiveTrade(player);
			if (trade != null)
			{
				trade.StopTrade(player);
			}
		}

		public void OnCycle(RoomEntity player)
		{

		}
	}
}
