using System;
using System.Threading.Tasks;
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
            BossGroup = new MultithreadEventLoopGroup(1);
            WorkerGroup = new MultithreadEventLoopGroup();
            Bootstrap = new ServerBootstrap();
            try
            {
                Bootstrap
                    .Group(BossGroup, WorkerGroup)
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
                BoundChannel = await Bootstrap.BindAsync(201);
                Console.WriteLine("Listening for Connections on Port " + 201);
				Console.ReadLine();
                await BoundChannel.CloseAsync();
            }
            catch (FormatException formatException)
            {
				Console.WriteLine("Port isn't a valid number!");
            }
            catch (ArgumentOutOfRangeException argumentException)
            {
				Console.WriteLine("Port is out of valid range.");
            }
            catch (AggregateException aggregateException)
            {
				Console.WriteLine("Port is already in use.");
            }
            finally
            {
                await Task.WhenAll(
                    BossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                    WorkerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            }
        }

        public void Stop()
        {
            BoundChannel.CloseAsync();
        }

    }
}
