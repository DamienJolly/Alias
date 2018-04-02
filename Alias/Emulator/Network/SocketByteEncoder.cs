using System;
using System.Collections.Generic;
using Alias.Emulator.Network.Protocol;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Alias.Emulator.Network
{
	public class SocketByteEncoder : MessageToMessageEncoder<ServerPacket>
	{
		protected override void Encode(IChannelHandlerContext context, ServerPacket message, List<Object> output)
		{
			if (!message.HasLength)
			{
				message.Buffer.SetInt(0, message.Buffer.WriterIndex - 4);
			}
			
			output.Add(message.Buffer);
		}
	}
}
