using System.Collections.Generic;
using Alias.Emulator.Hotel.Players.Wardrobe;
using Alias.Emulator.Hotel.Players.Wardrobe.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Wardrobe.Events
{
	class RequestUserWardrobeEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int slotsAvailable = session.Player.Wardrobe.SlotsAvailable;
			if (slotsAvailable == 0)
			{
				return;
			}

			IDictionary<int, WardrobeItem> items = new Dictionary<int, WardrobeItem>(session.Player.Wardrobe.WardobeItems);
			IDictionary<int, WardrobeItem> validItems = new Dictionary<int, WardrobeItem>();
			foreach (WardrobeItem Item in items.Values)
			{
				if (Item.SlotId > slotsAvailable)
				{
					continue;
				}

				validItems.Add(Item.SlotId, Item);
			}

			session.Send(new UserWardrobeComposer(validItems.Values));
		}
	}
}
