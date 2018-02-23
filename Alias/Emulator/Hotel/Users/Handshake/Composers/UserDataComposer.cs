using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class UserDataComposer : MessageComposer
	{
		private Habbo Habbo;

		public UserDataComposer(Habbo habbo)
		{
			this.Habbo = habbo;

			if (this.Habbo == null)
			{
				this.Habbo = new Habbo();
			}
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
