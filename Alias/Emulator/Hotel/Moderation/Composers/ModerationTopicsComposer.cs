using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationTopicsComposer : IMessageComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationTopicsMessageComposer);
			result.Int(1);
			result.String("Sexually Explicit");
			result.Int(2);
			result.String("test");
			result.Int(1);
			result.String("testing");
			result.String("test2");
			result.Int(2);
			result.String("testing2");
			return result;
		}
	}
}