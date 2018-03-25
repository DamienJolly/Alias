using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class BubbleAlertComposer : IPacketComposer
	{
		string Type;
		Dictionary<string, string> Data;

		public BubbleAlertComposer(string type, Dictionary<string, string> data)
		{
			this.Type = type;
			this.Data = data;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.BubbleAlertMessageComposer);
			message.String(this.Type);
			message.Int(this.Data.Count);
			this.Data.ToList().ForEach(part =>
			{
				message.String(part.Key);
				message.String(part.Value);
			});
			return message;
		}
	}
}
