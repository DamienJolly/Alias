using Alias.Emulator.Hotel.Groups;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	class RoomDataComposer : IPacketComposer
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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomDataMessageComposer);
			message.WriteBoolean(this.Loading);
			message.WriteInteger(this.Data.Id);
			message.WriteString(this.Data.Name);
			message.WriteInteger(this.Data.OwnerId);
			message.WriteString(this.Data.OwnerName);
			message.WriteInteger((int)this.Data.DoorState);
			message.WriteInteger(this.Data.UsersNow);
			message.WriteInteger(this.Data.MaxUsers);
			message.WriteString(this.Data.Description);
			message.WriteInteger((int)this.Data.TradeState);
			message.WriteInteger(this.Data.Likes.Count);
			message.WriteInteger(0);
			message.WriteInteger(this.Data.Category);
			message.WriteInteger(this.Data.Tags.Count);
			this.Data.Tags.ForEach(tag => message.WriteString(tag));
			message.WriteInteger(this.Data.EnumType);
			if (this.Data.Image.Length > 0)
			{
				message.WriteString(this.Data.Image);
			}
			
			if (this.Data.Group != null)
			{
				message.WriteInteger(this.Data.Group.Id);
				message.WriteString(this.Data.Group.Name);
				message.WriteString(this.Data.Group.Badge);
			}

			//62 -> group & promo // Id name badge // name description minutesleft
			//58 -> group // Id Name Badge
			//60 -> promo // name description minutesleft
			message.WriteBoolean(this.Entry);
			message.WriteBoolean(false); // staff picked
			message.WriteBoolean(false); // public room
			message.WriteBoolean(false); // is muted
			message.WriteInteger(this.Data.Settings.WhoMutes);
			message.WriteInteger(this.Data.Settings.WhoKicks);
			message.WriteInteger(this.Data.Settings.WhoBans);
			message.WriteBoolean(session.Player.Id == this.Data.OwnerId);
			message.WriteInteger(this.Data.Settings.ChatMode);
			message.WriteInteger(this.Data.Settings.ChatSize);
			message.WriteInteger(this.Data.Settings.ChatSpeed);
			message.WriteInteger(this.Data.Settings.ChatDistance);
			message.WriteInteger(this.Data.Settings.ChatFlood);
			return message;
		}
	}
}
