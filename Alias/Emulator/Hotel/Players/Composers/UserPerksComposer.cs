using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	class UserPerksComposer : IPacketComposer
	{
		private Player _player;

		public UserPerksComposer(Player player)
		{
			_player = player;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserPerksMessageComposer);
			message.WriteInteger(15);
			message.WriteString("USE_GUIDE_TOOL");
			message.WriteString("");
			message.WriteBoolean(false);
			message.WriteString("GIVE_GUIDE_TOURS");
			message.WriteString("requirement.unfulfilled.helper_le");
			message.WriteBoolean(false);
			message.WriteString("JUDGE_CHAT_REVIEWS");
			message.WriteString("");
			message.WriteBoolean(true);
			message.WriteString("VOTE_IN_COMPETITIONS");
			message.WriteString("");
			message.WriteBoolean(true);
			message.WriteString("CALL_ON_HELPERS");
			message.WriteString("");
			message.WriteBoolean(false);
			message.WriteString("CITIZEN");
			message.WriteString("");
			message.WriteBoolean(true);
			message.WriteString("TRADE");
			message.WriteString("");
			message.WriteBoolean(true);
			message.WriteString("HEIGHTMAP_EDITOR_BETA");
			message.WriteString("");
			message.WriteBoolean(false);
			message.WriteString("BUILDER_AT_WORK");
			message.WriteString("");
			message.WriteBoolean(true);
			message.WriteString("NAVIGATOR_PHASE_ONE_2014");
			message.WriteString("");
			message.WriteBoolean(false);
			message.WriteString("CAMERA");
			message.WriteString("");
			message.WriteBoolean(true);
			message.WriteString("NAVIGATOR_PHASE_TWO_2014");
			message.WriteString("");
			message.WriteBoolean(true);
			message.WriteString("MOUSE_ZOOM");
			message.WriteString("");
			message.WriteBoolean(true);
			message.WriteString("NAVIGATOR_ROOM_THUMBNAIL_CAMERA");
			message.WriteString("");
			message.WriteBoolean(true);
			message.WriteString("HABBO_CLUB_OFFER_BETA");
			message.WriteString("");
			message.WriteBoolean(true);
			//todo: all this crap
			return message;
		}
	}
}
