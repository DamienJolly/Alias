using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using DotNetty.Transport.Channels;

namespace Alias.Emulator.Network.Sessions
{
	class SessionManager
	{
		private Dictionary<IChannelHandlerContext, Session> _registeredSessions;

		public SessionManager()
		{
			this._registeredSessions = new Dictionary<IChannelHandlerContext, Session>();
		}

		public Habbo HabboById(int UserId)
		{
			if (this.IsOnline(UserId))
			{
				return this.SessionById(UserId).Habbo;
			}
			else
			{
				UserDatabase.ReadHabboData(UserId, out Habbo habbo);
				return habbo;
			}
		}

		public int OnlineUsers()
		{
			return this._registeredSessions.Values.Where(o => o.Habbo != null && !o.Habbo.IsDisconnecting).Count();
		}

		public bool IsOnline(int userId)
		{
			return this._registeredSessions.Values.Where(o => o.Habbo != null && o.Habbo.Id == userId && !o.Habbo.IsDisconnecting).Count() > 0;
		}

		public Session SessionById(int userId)
		{
			return this._registeredSessions.Values.Where(o => o.Habbo != null && o.Habbo.Id == userId).FirstOrDefault();
		}

		public void Register(IChannelHandlerContext context)
		{
			this._registeredSessions.Add(context, new Session(context));
		}

		public Session SessionByContext(IChannelHandlerContext context)
		{
			return this._registeredSessions[context];
		}

		public void Remove(IChannelHandlerContext context)
		{
			this._registeredSessions.Remove(context);
		}

		public void SendWithPermission(IPacketComposer message, string param)
		{
			this._registeredSessions.Values.Where(o => o.Habbo != null && !o.Habbo.IsDisconnecting && o.Habbo.HasPermission(param)).ToList().ForEach(o => o.Send(message));
		}

		public void Send(IPacketComposer message)
		{
			this._registeredSessions.Values.Where(o => o.Habbo != null && !o.Habbo.IsDisconnecting).ToList().ForEach(o => o.Send(message));
		}
	}
}
