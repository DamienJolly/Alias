using System;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Messages;

namespace Alias.Emulator.Network.Sessions
{
	public class Session
	{
		private IChannelHandlerContext context;

		public Session(IChannelHandlerContext ctx)
		{
			Console.WriteLine("Connection incoming from: " + ctx.Channel.LocalAddress);
			this.context = ctx;
		}

		public void Send(ServerMessage response, bool dispose = true)
		{
			byte[] array = response.ByteBuffer();
			this.Context().Channel.WriteAndFlushAsync(Unpooled.CopiedBuffer(array));
			if (dispose)
			{
				array = null;
				response.Dispose();
			}
		}

		public void Send(MessageComposer composer, bool dispose = true)
		{
			this.Send(composer.Compose(), dispose);
		}

		public IChannelHandlerContext Context()
		{
			return this.context;
		}

		public void Disconnect(bool closeSocket = true)
		{
			//todo: do habbo shit
			if (closeSocket)
			{
				this.Context().CloseAsync();
			}
		}
	}
}
