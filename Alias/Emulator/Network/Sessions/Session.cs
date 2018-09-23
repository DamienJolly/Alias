using DotNetty.Transport.Channels;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Packets;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Players;

namespace Alias.Emulator.Network.Sessions
{
	class Session
	{
		public string UniqueId { get; set; } = string.Empty;
		public Player Player { get; set; }
		private IChannelHandlerContext _context;

		public Session(IChannelHandlerContext ctx)
		{
			_context = ctx;
		}

		public void Send(ServerPacket response)
		{
			_context.Channel.WriteAndFlushAsync(response);
		}

		public void Send(IPacketComposer composer)
		{
			Send(composer.Compose());
		}

		public void Send(List<IPacketComposer> composer)
		{
			composer.ForEach(message =>
			{
				Send(message.Compose());
			});
		}

		public async void Disconnect(bool closeSocket = true)
		{
			if (Player != null)
			{
				Player.Dispose();
				Player = null;
			}
			UniqueId = string.Empty;
			if (closeSocket)
			{
				await _context.CloseAsync();
			}
		}
	}
}
