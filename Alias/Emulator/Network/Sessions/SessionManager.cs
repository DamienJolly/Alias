using System.Collections.Generic;
using DotNetty.Transport.Channels;

namespace Alias.Emulator.Network.Sessions
{
	public class SessionManager
	{
		private static Dictionary<IChannelHandlerContext, Session> RegisteredSessions;

		public static void Initialize()
		{
			RegisteredSessions = new Dictionary<IChannelHandlerContext, Session>();
		}

		//todo: do habbo shit

		public static void Register(IChannelHandlerContext context)
		{
			RegisteredSessions.Add(context, new Session(context));
		}

		public static Session SessionByContext(IChannelHandlerContext context)
		{
			return RegisteredSessions[context];
		}

		public static void Remove(IChannelHandlerContext context)
		{
			RegisteredSessions.Remove(context);
		}
	}
}
