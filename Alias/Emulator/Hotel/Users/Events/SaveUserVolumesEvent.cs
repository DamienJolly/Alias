using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class SaveUserVolumesEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			int system = message.Integer();
			int furni = message.Integer();
			int trax = message.Integer();

			session.Habbo().Settings.VolumeSystem = system;
			session.Habbo().Settings.VolumeFurni = furni;
			session.Habbo().Settings.VolumeTrax = trax;
		}
	}
}
