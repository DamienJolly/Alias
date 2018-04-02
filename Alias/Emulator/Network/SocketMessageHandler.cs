using Alias.Emulator.Network.Protocol;
using DotNetty.Transport.Channels;

namespace Alias.Emulator.Network
{
	public class SocketMessageHandler : SimpleChannelInboundHandler<ClientPacket>
	{
		public override void ChannelRegistered(IChannelHandlerContext context)
        {
			Alias.Server.SocketServer.SessionManager.Register(context);
			base.ChannelActive(context);
		}

        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
			Alias.Server.SocketServer.SessionManager.SessionByContext(context).Disconnect(false);
			Alias.Server.SocketServer.SessionManager.Remove(context);
			base.ChannelUnregistered(context);
		}

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
            base.ChannelReadComplete(context);
        }

        protected override void ChannelRead0(IChannelHandlerContext context, ClientPacket msg)
        {
			Alias.Server.SocketServer.PacketManager.Event(msg.Header).Handle(Alias.Server.SocketServer.SessionManager.SessionByContext(context), msg);
		}
    }
}
