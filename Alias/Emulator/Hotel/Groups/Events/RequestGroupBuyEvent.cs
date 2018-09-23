using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class RequestGroupBuyEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			string name = message.PopString();
			string description = message.PopString();

			if (name.Length <= 0)
			{
				return;
			}

			int roomId = message.PopInt();
			if (!Alias.Server.RoomManager.TryGetRoomData(roomId, out RoomData roomData))
			{
				return;
			}

			if (roomData.Group != null || roomData.OwnerId != session.Player.Id)
			{
				return;
			}

			int colorOne = message.PopInt();
			int colorTwo = message.PopInt();
			int count = message.PopInt();

			string badge = "";
			for (int i = 0; i < count; i += 3)
			{
				int id = message.PopInt();
				int colour = message.PopInt();
				int pos = message.PopInt();

				if (i == 0)
				{
					badge += "b";
				}
				else
				{
					badge += "s";
				}

				badge += (id < 100 ? "0" : "") + (id < 10 ? "0" : "") + id + (colour < 10 ? "0" : "") + colour + "" + pos;
			}

			Group group = Alias.Server.GroupManager.CreateGroup(session.Player, roomId, roomData.Name, name, description, badge, colorOne, colorTwo);
			roomData.Group = group;
			
			if (!session.Player.HasPermission("acc_infinite_credits"))
			{
				int guildPrice = 10;
				if (session.Player.Credits >= guildPrice)
				{
					session.Player.Credits -= guildPrice;
				}
				else
				{
					session.Send(new AlertPurchaseFailedComposer(AlertPurchaseFailedComposer.SERVER_ERROR));
					return;
				}
			}

			session.Send(new PurchaseOKComposer());
			session.Send(new GroupBoughtComposer(group));

			if (session.Player.CurrentRoom != null)
			{
				session.Player.CurrentRoom.UpdateGroup(group);
			}
		}
	}
}
