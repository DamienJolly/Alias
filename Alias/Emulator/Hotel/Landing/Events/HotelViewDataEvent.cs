using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	public class HotelViewDataEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			try
			{
				string data = message.String();
				if (data.Contains(";"))
				{
					string[] d = data.Split(';');

					foreach (string s in d)
					{
						if (s.Contains(","))
						{
							session.Send(new HotelViewDataComposer(s, s.Split(',')[s.Split(',').Length - 1]));
						}
						else
						{
							session.Send(new HotelViewDataComposer(data, s));
						}

						break;
					}
				}
				else
				{
					session.Send(new HotelViewDataComposer(data, data.Split(',')[data.Split(',').Length - 1]));
				}
			}
			catch { }
		}
	}
}
