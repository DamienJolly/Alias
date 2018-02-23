using System;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Network
{
	public class ChannelHandler : ChannelHandlerAdapter
	{
		public override void ChannelRead(IChannelHandlerContext context, object message)
		{
			IByteBuffer buffer = message as IByteBuffer;
			if (buffer != null)
			{
				// Crossdomain stuff
				if (buffer.ToString(Encoding.UTF8).ToLower().StartsWith("<policy-file-request/>"))
				{
					context.Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(Constant.PolicyFile)).ContinueWith(delegate { context.CloseAsync(); });
					return;
				}

				while (buffer.ReadableBytes > 0)
				{
					try
					{
						if (buffer.ReadableBytes < 4)
						{
							return;
						}
						buffer.MarkReaderIndex();
						int length = buffer.ReadInt();
						if (buffer.ReadableBytes < length)
						{
							buffer.ResetReaderIndex();
							return;
						}
						ClientMessage clientMessage = new ClientMessage(buffer.ReadBytes(length));
						MessageHandler.Event(clientMessage.Id).Handle(SessionManager.SessionByContext(context), clientMessage);
						clientMessage.Dispose();
					}
					catch (Exception exception)
					{
						Logging.Error("Error while reading ClientMessage", exception, "ChannelHandler", "ChannelRead");
						return;
					}
				}
			}
		}

		public override void ChannelReadComplete(IChannelHandlerContext context)
		{
			base.ChannelReadComplete(context);
		}

		public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
		{
			base.ExceptionCaught(context, exception);
			SessionManager.SessionByContext(context).Disconnect();
		}

		public override void ChannelRegistered(IChannelHandlerContext context)
		{
			SessionManager.Register(context);
			base.ChannelRegistered(context);
		}

		public override void ChannelUnregistered(IChannelHandlerContext context)
		{
			SessionManager.SessionByContext(context).Disconnect(false);
			SessionManager.Remove(context);
			base.ChannelUnregistered(context);
		}
	}
}
