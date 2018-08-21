using System.Collections.Generic;
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
                context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Constant.PolicyFile));
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
    }
}
