using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Trading.Composers
{
    class TradeUpdateComposer : IPacketComposer
	{
		private RoomTrade roomTrade;

		public TradeUpdateComposer(RoomTrade roomTrade)
		{
			this.roomTrade = roomTrade;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.TradeUpdateMessageComposer);
			roomTrade.Users.ForEach(user =>
			{
				message.WriteInteger(user.User.Habbo.Id);
				message.WriteInteger(user.OfferedItems.Count);
				user.OfferedItems.ForEach(item =>
				{
					message.WriteInteger(item.Id);
					message.WriteString(item.ItemData.Type.ToUpper());
					message.WriteInteger(item.Id);
					message.WriteInteger(item.ItemData.SpriteId);
					message.WriteInteger(0);
					message.WriteBoolean(item.ItemData.CanStack && !item.IsLimited);
					message.WriteInteger(item.IsLimited ? 256 : 0);
					message.WriteString(item.Mode.ToString());
					if (item.IsLimited)
					{
						message.WriteInteger(item.LimitedNumber);
						message.WriteInteger(item.LimitedStack);
					}
					message.WriteInteger(0);
					message.WriteInteger(0);
					message.WriteInteger(0);

					if (item.ItemData.Type.ToUpper() == "S")
					{
						message.WriteInteger(0);
					}
				});
				message.WriteInteger(user.OfferedItems.Count);
				message.WriteInteger(0); // todo: exchange items value
			});
			return message;
		}
	}
}
