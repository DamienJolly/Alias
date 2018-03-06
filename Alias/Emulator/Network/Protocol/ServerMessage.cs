using System;
using System.Collections.Generic;
using System.Text;

namespace Alias.Emulator.Network.Protocol
{
	public class ServerMessage
	{
		private List<byte> Message;
		private int MessageId;

		public ServerMessage(int Header)
		{
			this.Message = new List<byte>();
			this.MessageId = Header;
			this.Short(Header);
		}

		public byte[] ByteBuffer()
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(this.Message.Count));
			list.Reverse();
			list.AddRange(this.Message);
			return list.ToArray();
		}

		public void Bytes(byte[] b, bool IsInt)
		{
			if (IsInt)
			{
				for (int i = b.Length - 1; i > -1; i--)
				{
					this.Message.Add(b[i]);
				}
			}
			else
			{
				this.Message.AddRange(b);
			}
		}

		public void String(string s)
		{
			this.Short(s.Length);
			this.Bytes(Encoding.UTF8.GetBytes(s), false);
		}

		public void Short(int i)
		{
			short num = (short)i;
			this.Bytes(BitConverter.GetBytes(num), true);
		}

		public void Byte(int i)
		{
			Message.Add((byte)i);
		}

		public void Int(int i)
		{
			this.Bytes(BitConverter.GetBytes(i), true);
		}

		public void Boolean(bool b)
		{
			this.Bytes(new byte[] { b ? ((byte)1) : ((byte)0) }, false);
		}

		public void Dispose()
		{
			this.Message.Clear();
		}
	}
}
