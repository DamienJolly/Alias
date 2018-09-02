using System.Collections.Generic;
using System.Text;
using Alias.Emulator.Network.Protocol;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Alias.Emulator.Network
{
	public class SocketByteDecoder : ByteToMessageDecoder
	{
		protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
			input.MarkReaderIndex();

			if (input.ReadableBytes < 6)
			{
				return;
			}

            byte delimeter = input.ReadByte();
            input.ResetReaderIndex();

            if (delimeter == 60)
            {
                context.WriteAndFlushAsync(Unpooled.CopiedBuffer(PolicyRequest));
            }
            else
            {
                input.MarkReaderIndex();
                int length = input.ReadInt();
                if (input.ReadableBytes < length)
                {
                    input.ResetReaderIndex();
                    return;
                }
                IByteBuffer buffer = input.ReadBytes(length);

				if (length < 0)
				{
					return;
				}
				ClientPacket clientMessage = new ClientPacket(buffer.ReadBytes(length));
                output.Add(clientMessage);
            }
        }

		private byte[] PolicyRequest => Encoding.UTF8.GetBytes("<?xml version=\"1.0\"?>\r\n<!DOCTYPE cross-domain-policy SYSTEM \"/xml/dtds/cross-domain-policy.dtd\">\r\n<cross-domain-policy>\r\n<allow-access-from domain=\"*\" to-ports=\"1-31111\" />\r\n</cross-domain-policy>\0");
	}
}
