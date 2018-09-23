using Alias.Emulator.Hotel.Groups;
using Alias.Emulator.Hotel.Rooms.Trading;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Entities.Types
{
    class EntityPlayer : IEntityType
    {
		public override void Serialize(ServerPacket message)
		{
			message.WriteInteger(1);
			message.WriteString(Entity.Gender.ToLower());

			Group group = Alias.Server.GroupManager.GetGroup(Entity.Player.GroupId);
			message.WriteInteger(group != null ? group.Id : -1);
			message.WriteInteger(0); //??
			message.WriteString(group != null ? group.Name : "");

			message.WriteString("");
			message.WriteInteger(Entity.Player.AchievementScore);
			message.WriteBoolean(false); //idk
		}

		public override void OnEntityJoin()
		{
			Entity.Player.Entity = Entity;
			CurrentRoom.RoomData.UsersNow++;
		}

		public override void OnEntityLeave()
		{
			Entity.Player.CurrentRoom = null;
			Entity.Player.Entity = null;
			CurrentRoom.RoomData.UsersNow--;

			RoomTrade trade = CurrentRoom.RoomTrading.GetActiveTrade(Entity);
			if (trade != null)
			{
				trade.StopTrade(Entity);
			}
		}

		public override void OnCycle()
		{

		}
	}
}
