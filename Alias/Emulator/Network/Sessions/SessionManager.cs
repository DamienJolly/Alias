using System.Collections.Generic;
using Alias.Emulator.Network.Packets;
using DotNetty.Transport.Channels;

namespace Alias.Emulator.Network.Sessions
{
	class SessionManager
	{
		private readonly Dictionary<IChannelHandlerContext, Session> _registeredSessions;

		public SessionManager()
		{
			_registeredSessions = new Dictionary<IChannelHandlerContext, Session>();
		}

		public void Register(IChannelHandlerContext context)
		{
			_registeredSessions.Add(context, new Session(context));
		}

		public Session SessionByContext(IChannelHandlerContext context)
		{
			return _registeredSessions[context];
		}

		public void Remove(IChannelHandlerContext context)
		{
			_registeredSessions.Remove(context);
		}

		public void SendWithPermission(IPacketComposer message, string param)
		{
			foreach (Session session in _registeredSessions.Values)
			{
				if (session.Player == null || session.Player.IsDisconnecting)
				{
					continue;
				}

				if (session.Player.HasPermission(param))
				{
					session.Send(message);
				}
			}
		}

		public void Send(IPacketComposer message)
		{
			foreach (Session session in _registeredSessions.Values)
			{
				if (session.Player == null || session.Player.IsDisconnecting)
				{
					continue;
				}

				session.Send(message);
			}
		}
	}
}
