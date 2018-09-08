using Alias.Emulator.Hotel.Groups;
using Alias.Emulator.Hotel.Rooms.Trading;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Types
{
    class EntityPlayer : IEntityType
    {
		public void Serialize(ServerPacket message, RoomEntity player)
		{
			message.WriteInteger(1);
			message.WriteString(player.Gender.ToLower());

			Group group = Alias.Server.GroupManager.GetGroup(player.Habbo.GroupId);
			message.WriteInteger(group != null ? group.Id : -1);
			message.WriteInteger(0); //??
			message.WriteString(group != null ? group.Name : "");

			message.WriteString("");
			message.WriteInteger(player.Habbo.AchievementScore);
			message.WriteBoolean(false); //idk
		}

		public void OnEntityJoin(RoomEntity player)
		{
			player.Habbo.Entity = player;
			player.Room.RoomData.UsersNow++;
		}

		public void OnEntityLeave(RoomEntity player)
		{
			player.Habbo.CurrentRoom = null;
			player.Habbo.Entity = null;
			player.Room.RoomData.UsersNow--;

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
