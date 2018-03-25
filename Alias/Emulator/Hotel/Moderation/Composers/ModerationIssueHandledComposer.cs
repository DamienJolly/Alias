using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationIssueHandledComposer : IPacketComposer
	{
		public static int HANDLED = 0;
		public static int USELESS = 1;
		public static int ABUSIVE = 2;

		private int code = 0;
		private string message = "";

		public ModerationIssueHandledComposer(int code)
		{
			this.code = code;
		}

		public ModerationIssueHandledComposer(string message)
		{
			this.message = message;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationIssueHandledMessageComposer);
			result.Int(this.code);
			result.String(this.message);
			return result;
		}
	}
}
