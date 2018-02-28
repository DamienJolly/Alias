using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class HallOfFameComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.HallOfFameMessageComposer);
			message.String("testing");
			message.Int(1);
			message.Int(1);
			message.String("Damien");
			message.String("ha-1006-64.lg-275-64.hd-209-1370.ch-3030-82");
			message.Int(1);
			message.Int(69);
			return message;
		}
	}
}
