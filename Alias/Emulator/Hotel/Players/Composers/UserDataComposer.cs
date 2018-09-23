using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	class UserDataComposer : IPacketComposer
	{
		private Player _player;

		public UserDataComposer(Player player)
		{
			_player = player;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserDataMessageComposer);
			message.WriteInteger(_player.Id);
			message.WriteString(_player.Username);
			message.WriteString(_player.Look);
			message.WriteString(_player.Gender.ToUpper());
			message.WriteString(_player.Motto);
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
