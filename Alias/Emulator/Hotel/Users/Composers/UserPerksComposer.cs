using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserPerksComposer : IMessageComposer
	{
		private Habbo Habbo;

		public UserPerksComposer(Habbo habbo)
		{
			this.Habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.UserPerksMessageComposer);
			message.Int(15);
			message.String("USE_GUIDE_TOOL");
			message.String("");
			message.Boolean(false);
			message.String("GIVE_GUIDE_TOURS");
			message.String("requirement.unfulfilled.helper_le");
			message.Boolean(false);
			message.String("JUDGE_CHAT_REVIEWS");
			message.String("");
			message.Boolean(true);
			message.String("VOTE_IN_COMPETITIONS");
			message.String("");
			message.Boolean(true);
			message.String("CALL_ON_HELPERS");
			message.String("");
			message.Boolean(false);
			message.String("CITIZEN");
			message.String("");
			message.Boolean(true);
			message.String("TRADE");
			message.String("");
			message.Boolean(true);
			message.String("HEIGHTMAP_EDITOR_BETA");
			message.String("");
			message.Boolean(false);
			message.String("BUILDER_AT_WORK");
			message.String("");
			message.Boolean(true);
			message.String("NAVIGATOR_PHASE_ONE_2014");
			message.String("");
			message.Boolean(false);
			message.String("CAMERA");
			message.String("");
			message.Boolean(true);
			message.String("NAVIGATOR_PHASE_TWO_2014");
			message.String("");
			message.Boolean(true);
			message.String("MOUSE_ZOOM");
			message.String("");
			message.Boolean(true);
			message.String("NAVIGATOR_ROOM_THUMBNAIL_CAMERA");
			message.String("");
			message.Boolean(true);
			message.String("HABBO_CLUB_OFFER_BETA");
			message.String("");
			message.Boolean(true);
			//todo: all this crap
			return message;
		}
	}
}
