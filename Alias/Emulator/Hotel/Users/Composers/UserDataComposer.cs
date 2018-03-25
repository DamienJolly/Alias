using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserDataComposer : IPacketComposer
	{
		private Habbo Habbo;

		public UserDataComposer(Habbo habbo)
		{
			this.Habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.UserDataMessageComposer);
			message.Int(this.Habbo.Id);
			message.String(this.Habbo.Username);
			message.String(this.Habbo.Look);
			message.String(this.Habbo.Gender.ToUpper());
			message.String(this.Habbo.Motto);
			message.String("");
			message.Boolean(false);
			message.Int(0); //Respect
			message.Int(0); //DailyRespect
			message.Int(0); //DailyPetRespect
			message.Boolean(false); //Friendstream
			message.String("01.01.1970 00:00:00"); //Last Online
			message.Boolean(false); //Can change name
			message.Boolean(false);
			//todo: All this crap
			return message;
		}
	}
}
