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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.BubbleAlertMessageComposer);
			message.WriteString(this.Type);
			message.WriteInteger(this.Data.Count);
			this.Data.ToList().ForEach(part =>
			{
				message.WriteString(part.Key);
				message.WriteString(part.Value);
			});
			return message;
		}
	}
}
