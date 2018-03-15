using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Users.Currency.Composers;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	public class RedeemItemEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}
			
			int itemId = message.Integer();
			RoomItem rItem = room.ItemManager.GetItem(itemId);
			if (rItem == null || rItem.Owner != session.Habbo.Id)
			{
				return;
			}

			switch (rItem.ItemData.Interaction)
			{
				case ItemInteraction.EXCHANGE:
					{
						session.Habbo.Credits += rItem.ItemData.BehaviourData;
						session.Send(new UserCreditsComposer(session.Habbo));
						break;
					}
				case ItemInteraction.DIAMOND_EXCHANGE:
					{
						// todo:
						break;
					}
				case ItemInteraction.POINTS_EXCHANGE:
					{
						session.Habbo.Currency.GetCurrencyType(1).Amount += rItem.ItemData.BehaviourData;
						session.Send(new UserPointsComposer(session.Habbo.Currency.GetCurrencyType(1).Amount, +rItem.ItemData.BehaviourData, 1));

						session.Habbo.Credits += rItem.ItemData.BehaviourData;
						session.Send(new UserCreditsComposer(session.Habbo));
						break;
					}
				default:
					return;
			}

			room.ItemManager.RemoveItem(rItem);
			room.UserManager.Send(new RemoveFloorItemComposer(rItem));
		}
	}
}
