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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserDataMessageComposer);
			message.WriteInteger(this.Habbo.Id);
			message.WriteString(this.Habbo.Username);
			message.WriteString(this.Habbo.Look);
			message.WriteString(this.Habbo.Gender.ToUpper());
			message.WriteString(this.Habbo.Motto);
			message.WriteString("");
			message.WriteBoolean(false);
			message.WriteInteger(0); //Respect
			message.WriteInteger(0); //DailyRespect
			message.WriteInteger(0); //DailyPetRespect
			message.WriteBoolean(false); //Friendstream
			message.WriteString("01.01.1970 00:00:00"); //Last Online
			message.WriteBoolean(false); //Can change name
			message.WriteBoolean(false);
			//todo: All this crap
			return message;
		}
	}
}
