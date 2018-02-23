using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Utilities;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Network.Sessions
{
	public class Session
	{
		private IChannelHandlerContext context;
		public string UniqueId = "";
		private Habbo habbo;

		public Session(IChannelHandlerContext ctx)
		{
			Logging.Info("Connection incoming from: " + ctx.Channel.LocalAddress);
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
			if (this.Habbo() != null)
			{
				this.Habbo().OnDisconnect();
			}
			if (closeSocket)
			{
				this.Context().CloseAsync();
			}
		}

		public Habbo Habbo()
		{
			return this.habbo;
		}

		public void AssignHabbo(Habbo habbo)
		{
			this.habbo = habbo;
		}
	}
}
