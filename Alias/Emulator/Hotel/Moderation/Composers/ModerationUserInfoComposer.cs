using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	class ModerationUserInfoComposer : IPacketComposer
	{
		private Player habbo;

		public ModerationUserInfoComposer(Player habbo)
		{
			this.habbo = habbo;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ModerationUserInfoMessageComposer);
			message.WriteInteger(this.habbo.Id);
			message.WriteString(this.habbo.Username);
			message.WriteString(this.habbo.Look);
			message.WriteInteger(0); //account created
			message.WriteInteger(0); //last online
			message.WriteBoolean(true); //isOnline
			message.WriteInteger(0); //cfh sent
			message.WriteInteger(0); //cfh abuse
			message.WriteInteger(0); //cfh warning
			message.WriteInteger(0); //cfh bans
			message.WriteInteger(0); //trade locks
			message.WriteString(string.Empty); //trading lock
			message.WriteString(string.Empty); //purchases
			message.WriteInteger(0); //??
			message.WriteInteger(0); //??
			message.WriteString(this.habbo.Mail);
			message.WriteString(string.Empty); //??
			return message;
		}
	}
}
