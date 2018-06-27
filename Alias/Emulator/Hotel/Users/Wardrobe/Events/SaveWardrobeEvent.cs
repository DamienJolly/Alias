using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Wardrobe.Events
{
	class SaveWardrobeEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int slotsAvailable = session.Habbo.Wardrobe.SlotsAvailable;

			if (slotsAvailable == 0)
			{
				return;
			}

			int slotId = message.PopInt();
			string look = message.PopString();
			string gender = message.PopString().ToUpper();

			if (slotId <= 0 || slotId > slotsAvailable)
			{
				return;
			}

			if (!FigureValidation.Validate(look, gender))
			{
				return;
			}

			if (session.Habbo.Wardrobe.TryGet(slotId, out WardrobeItem Item))
			{
				Item.Figure = look;
				Item.Gender = gender;
				WardrobeDatabase.UpdateWardrobeItem(Item);
			}
			else
			{
				WardrobeItem NewItem = new WardrobeItem(session.Habbo.Id, slotId, look, gender);
				if (session.Habbo.Wardrobe.TryAdd(slotId, Item))
				{
					WardrobeDatabase.InsertWardrobeItem(NewItem);
				}
			}
		}
	}
}
