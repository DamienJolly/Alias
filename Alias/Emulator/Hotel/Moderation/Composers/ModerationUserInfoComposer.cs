using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationUserInfoComposer : IPacketComposer
	{
		private Habbo habbo;

		public ModerationUserInfoComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationUserInfoMessageComposer);
			result.Int(this.habbo.Id);
			result.String(this.habbo.Username);
			result.String(this.habbo.Look);
			result.Int(0); //account created
			result.Int(0); //last online
			result.Boolean(true); //isOnline
			result.Int(0); //cfh sent
			result.Int(0); //cfh abuse
			result.Int(0); //cfh warning
			result.Int(0); //cfh bans
			result.Int(0); //trade locks
			result.String(string.Empty); //trading lock
			result.String(string.Empty); //purchases
			result.Int(0); //??
			result.Int(0); //??
			result.String(this.habbo.Mail);
			result.String(string.Empty); //??
			return result;
		}
	}
}
