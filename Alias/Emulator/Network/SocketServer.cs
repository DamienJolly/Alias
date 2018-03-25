using System;
using System.Threading.Tasks;
using Alias.Emulator.Utilities;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace Alias.Emulator.Network
{
    class SocketServer
    {
		private static ServerBootstrap Bootstrap;
		private static MultithreadEventLoopGroup BossGroup;
		private static MultithreadEventLoopGroup WorkerGroup;
		private static IChannel BoundChannel;

		public static async void Initialize()
        {
			SocketServer.BossGroup   = new MultithreadEventLoopGroup(1);
			SocketServer.WorkerGroup = new MultithreadEventLoopGroup();
			SocketServer.Bootstrap   = new ServerBootstrap();
            try
            {
				SocketServer.Bootstrap
					.Group(SocketServer.BossGroup, SocketServer.WorkerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.AutoRead, true)
                    .Option(ChannelOption.SoBacklog, 100)
                    .Option(ChannelOption.SoKeepalive, true)
                    .Option(ChannelOption.ConnectTimeout, TimeSpan.MaxValue)
                    .Option(ChannelOption.TcpNodelay, false)
                    .Option(ChannelOption.SoRcvbuf, 5120)
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast("channel-handler", new ChannelHandler());
                    }));
				SocketServer.BoundChannel = await Bootstrap.BindAsync(int.Parse(Configuration.Value("tcp.port")));
				Logging.Info("Listening for Connections on Port " + Configuration.Value("tcp.port"));
				Console.ReadLine();
				await SocketServer.BoundChannel.CloseAsync();
            }
            catch (FormatException formatException)
            {
				Logging.Error("Port isn't a valid number!", formatException);
			}
            catch (ArgumentOutOfRangeException argumentException)
            {
				Logging.Error("Port is out of valid range.", argumentException);
			}
            catch (AggregateException aggregateException)
            {
				Logging.Error("Port is already in use.", aggregateException);
			}
            finally
            {
                await Task.WhenAll(
					SocketServer.BossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
					SocketServer.WorkerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            }
        }

        public void Stop()
        {
			SocketServer.BoundChannel.CloseAsync();
        }
    }
}
