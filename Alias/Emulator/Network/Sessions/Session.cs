using DotNetty.Transport.Channels;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Network.Sessions
{
	class Session
	{
		public string UniqueId
		{
			get; set;
		} = string.Empty;

		public Habbo Habbo
		{
			get; set;
		}

		private IChannelHandlerContext Context;

		public Session(IChannelHandlerContext ctx)
		{
			this.Context = ctx;
		}

		public void Send(ServerPacket response, bool dispose = true)
		{
			this.Context.Channel.WriteAndFlushAsync(response);
		}

		public void Send(IPacketComposer composer, bool dispose = true)
		{
			this.Send(composer.Compose(), dispose);
		}

		public void Disconnect(bool closeSocket = true)
		{
			if (this.Habbo != null)
			{
				this.Habbo.OnDisconnect();
				this.Habbo = null;
			}
			this.UniqueId = string.Empty;
			if (closeSocket)
			{
				this.Context.CloseAsync();
			}
		}

		public void AssignHabbo(Habbo habbo)
		{
			this.Habbo = habbo;
		}
	}
}
