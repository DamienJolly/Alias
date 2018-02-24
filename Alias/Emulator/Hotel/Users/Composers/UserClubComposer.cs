using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserClubComposer : MessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.UserClubMessageComposer);
			result.String("club_habbo");
			result.Int(0);
			result.Int(7);
			result.Int(0);
			result.Int(1);
			result.Boolean(true);
			result.Boolean(true);
			result.Int(0);
			result.Int(0);
			result.Int(int.MaxValue);
			return result;
		}
	}
}
