using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Handshake;
using Alias.Emulator.Network.Messages;
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

		public static Habbo HabboById(int UserId)
		{
			return SessionManager.IsOnline(UserId) ? SessionManager.SessionById(UserId).Habbo : HandshakeDatabase.BuildHabbo(UserId);
		}

		public static int OnlineUsers()
		{
			return SessionManager.RegisteredSessions.Values.Where(o => o.Habbo != null && !o.Habbo.IsDisconnecting).Count();
		}

		public static bool IsOnline(int userId)
		{
			return SessionManager.RegisteredSessions.Values.Where(o => o.Habbo != null && o.Habbo.Id == userId && !o.Habbo.IsDisconnecting).Count() > 0;
		}

		public static Session SessionById(int userId)
		{
			return SessionManager.RegisteredSessions.Values.Where(o => o.Habbo != null && o.Habbo.Id == userId).FirstOrDefault();
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

		public static void SendWithPermission(IMessageComposer message, string param)
		{
			SessionManager.RegisteredSessions.Values.Where(o => o.Habbo != null && !o.Habbo.IsDisconnecting && o.Habbo.HasPermission(param)).ToList().ForEach(o => o.Send(message));
		}

		public static void Send(IMessageComposer message)
		{
			SessionManager.RegisteredSessions.Values.Where(o => o.Habbo != null && !o.Habbo.IsDisconnecting).ToList().ForEach(o => o.Send(message));
		}
	}
}
