using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomDataComposer : IPacketComposer
	{
		private RoomData Data;
		private bool Loading;
		private bool Entry;
		private Session session;

		public RoomDataComposer(RoomData data, bool loading, bool entry, Session s)
		{
			this.Data = data;
			this.Loading = loading;
			this.Entry = entry;
			this.session = s;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.RoomDataMessageComposer);
			message.Boolean(this.Loading);
			message.Int(this.Data.Id);
			message.String(this.Data.Name);
			message.Int(this.Data.OwnerId);
			message.String(this.Data.OwnerName);
			message.Int(Alias.GetServer().GetRoomManager().DoorToInt(this.Data.DoorState));
			message.Int(this.Data.UsersNow);
			message.Int(this.Data.MaxUsers);
			message.String(this.Data.Description);
			message.Int(Alias.GetServer().GetRoomManager().TradeToInt(this.Data.TradeState));
			message.Int(this.Data.Likes.Count);
			message.Int(0);
			message.Int(this.Data.Category);
			message.Int(this.Data.Tags.Count);
			this.Data.Tags.ForEach(tag => message.String(tag));
			message.Int(this.Data.EnumType);
			if (this.Data.Image.Length > 0)
			{
				message.String(this.Data.Image);
			}
			//62 -> group & promo // Id name badge // name description minutesleft
			//58 -> group // Id Name Badge
			//60 -> promo // name description minutesleft
			message.Boolean(this.Entry);
			message.Boolean(false); // staff picked
			message.Boolean(true); // public room
			message.Boolean(false); // is muted
			message.Int(this.Data.Settings.WhoMutes);
			message.Int(this.Data.Settings.WhoKicks);
			message.Int(this.Data.Settings.WhoBans);
			message.Boolean(session.Habbo.Id == this.Data.OwnerId);
			message.Int(this.Data.Settings.ChatMode);
			message.Int(this.Data.Settings.ChatSize);
			message.Int(this.Data.Settings.ChatSpeed);
			message.Int(this.Data.Settings.ChatDistance);
			message.Int(this.Data.Settings.ChatFlood);
			return message;
		}
	}
}
