using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Interactions
{
	public class InteractionWired : IItemInteractor
	{
		public void Serialize(ServerPacket message, RoomItem item)
		{
			message.WriteInteger(item.IsLimited ? 256 : 0);
			message.WriteString(item.Mode.ToString());
		}

		public void OnUserEnter(RoomUser user, RoomItem item)
		{

		}

		public void OnUserLeave(RoomUser user, RoomItem item)
		{

		}

		public void OnUserWalkOn(Session session, Room room, RoomItem item)
		{
			if (item.ItemData.WiredInteraction == WiredInteraction.WALKS_ON_FURNI)
			{
				item.GetWiredInteractor().OnTrigger(null);
			}
		}

		public void OnUserWalkOff(Session session, Room room, RoomItem item)
		{
			if (item.ItemData.WiredInteraction == WiredInteraction.WALKS_OFF_FURNI)
			{
				item.GetWiredInteractor().OnTrigger(null);
			}
		}

		public void OnUserInteract(Session session, Room room, RoomItem item, int state)
		{
			if (!room.RoomRights.HasRights(session.Habbo.Id))
			{
				return;
			}

			if (item.ItemData.Interaction == ItemInteraction.WIRED_TRIGGER)
			{
				//session.Send(new WiredTriggerDataComposer(item));
			}
			else if (item.ItemData.Interaction == ItemInteraction.WIRED_EFFECT)
			{
				//session.Send(new WiredEffectDataComposer(item));
			}
			else if (item.ItemData.Interaction == ItemInteraction.WIRED_CONDITION)
			{
				//session.Send(new WiredConditionDataComposer(item));
			}
		}

		public void OnCycle(RoomItem item)
		{
			if (item.ItemData.WiredInteraction == WiredInteraction.REPEATER)
			{
				item.GetWiredInteractor().OnTrigger(null);
			}
		}
	}
}
