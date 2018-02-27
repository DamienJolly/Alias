using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Handshake;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using DotNetty.Transport.Channels;

namespace Alias.Emulator.Network.Sessions
{
	public class SessionManager
	{
		private static Dictionary<IChannelHandlerContext, Session> RegisteredSessions;

		public static void Initialize()
		{
			SessionManager.RegisteredSessions = new Dictionary<IChannelHandlerContext, Session>();
		}

		public static Habbo Habbo(int UserId)
		{
			if (SessionManager.IsOnline(UserId))
			{
				return SessionManager.SessionById(UserId).Habbo();
			}
			else
			{
				return HandshakeDatabase.BuildHabbo(UserId);
			}
		}

		public static int OnlineUsers()
		{
			return SessionManager.RegisteredSessions.Values.Where(o => o.Habbo() != null && !o.Habbo().Disconnecting).Count();
		}

		public static bool IsOnline(int userId)
		{
			return SessionManager.RegisteredSessions.Values.Where(o => o.Habbo() != null && o.Habbo().Id == userId && !o.Habbo().Disconnecting).Count() > 0;
		}

		public static Session SessionById(int userId)
		{
			return SessionManager.RegisteredSessions.Values.Where(o => o.Habbo() != null && o.Habbo().Id == userId).First();
		}

		public static void Register(IChannelHandlerContext context)
		{
			SessionManager.RegisteredSessions.Add(context, new Session(context));
		}

		public static Session SessionByContext(IChannelHandlerContext context)
		{
			return SessionManager.RegisteredSessions[context];
		}

		public static void Remove(IChannelHandlerContext context)
		{
			SessionManager.RegisteredSessions.Remove(context);
		}

		public static void Send(MessageComposer message)
		{
			SessionManager.RegisteredSessions.Values.Where(o => o.Habbo() != null && !o.Habbo().Disconnecting).ToList().ForEach(o => o.Send(message));
		}
	}
}
