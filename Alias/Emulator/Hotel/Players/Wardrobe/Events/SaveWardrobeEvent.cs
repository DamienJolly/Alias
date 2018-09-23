using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Wardrobe.Events
{
	class SaveWardrobeEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			int slotsAvailable = session.Player.Wardrobe.SlotsAvailable;

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

			if (session.Player.Wardrobe.TryGetWardrobeItem(slotId, out WardrobeItem item))
			{
				item.Figure = look;
				item.Gender = gender;
				await session.Player.Wardrobe.UpdateWardrobeItem(item);
			}
			else
			{
				WardrobeItem NewItem = new WardrobeItem(slotId, look, gender);
				await session.Player.Wardrobe.AddWardrobeItem(item);
			}
		}
	}
}
