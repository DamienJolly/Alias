using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserProfileComposer : MessageComposer
	{
		private Habbo habbo;
		private Session viewer;

		public UserProfileComposer(Habbo habbo, Session viewer)
		{
			this.habbo = habbo;
			this.viewer = viewer;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.UserProfileMessageComposer);
			result.Int(habbo.Id);
			result.String(habbo.Username);
			result.String(habbo.Look);
			result.String(habbo.Motto);
			result.String("01.01.1970 00:00:00"); //Account created
			result.Int(0); //Achievement score
			result.Int(SessionManager.IsOnline(habbo.Id) ? habbo.Messenger().FriendList().Count : 0); //todo: get friends amount for offline users
			result.Boolean(viewer.Habbo().Messenger().IsFriend(habbo.Id));
			result.Boolean(viewer.Habbo().Messenger().RequestExists(habbo.Id));
			result.Boolean(SessionManager.IsOnline(habbo.Id));

			result.Int(0); // Count - groups
			{
				// int    - group id
				// string - group name
				// string - group badge
				// string - group colour1
				// string - group colour2
				// bool   - Fav. group
				// int    - group owner id
				// bool   - is owner
			}

			result.Int(0); //Last online (seconds)
			result.Boolean(true);
			return result;
		}
	}
}
