using System;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Utilities;
using DotNetty.Buffers;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace Alias.Emulator.Network
{
    class SocketServer
    {
		public PacketManager PacketManager { get; }
		public SessionManager SessionManager { get; }

		private ServerBootstrap serverBootstrap;
		private IEventLoopGroup bossGroup;
		private IEventLoopGroup workerGroup;

		private string host;
		private int port;

		public SocketServer(string host, int port)
		{
			this.PacketManager = new PacketManager();
			this.SessionManager = new SessionManager();

			this.bossGroup = new MultithreadEventLoopGroup(1);
			this.workerGroup = new MultithreadEventLoopGroup(10);

			this.serverBootstrap = new ServerBootstrap();

			this.host = host;
			this.port = port;
		}

		public void Initialize()
		{
			this.serverBootstrap.Group(this.bossGroup, this.workerGroup);
			this.serverBootstrap.Channel<TcpServerSocketChannel>();
			this.serverBootstrap.ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
			{
				channel.Pipeline.AddLast("logger", new LoggingHandler());
				channel.Pipeline.AddLast("bytesEncoder", new SocketByteEncoder());
				channel.Pipeline.AddLast("bytesDecoder", new SocketByteDecoder());
				channel.Pipeline.AddLast("MessageHandler", new SocketMessageHandler());
			}));
			this.serverBootstrap.ChildOption(ChannelOption.TcpNodelay, true);
			this.serverBootstrap.ChildOption(ChannelOption.SoKeepalive, true);
			this.serverBootstrap.ChildOption(ChannelOption.SoRcvbuf, 5120);
			this.serverBootstrap.ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(5120));
			this.serverBootstrap.ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default);
		}

		public void Connect()
		{
			this.serverBootstrap.BindAsync(this.port);
			Logging.Debug($"Listening for Connections on port: {this.port}");
		}

		public void Stop()
		{
			try
			{
				this.workerGroup.ShutdownGracefullyAsync();
				this.bossGroup.ShutdownGracefullyAsync();
			}
			catch (Exception e)
			{
				Logging.Error("Exception shutting down Server..", e);
			}
		}
	}
}
