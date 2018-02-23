using System;
using System.Collections.Generic;
using System.Text;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Network.Protocol
{
	public class ServerMessage
	{
		private List<byte> Message;
		private int MessageId;

		public ServerMessage()
		{
			this.Message = new List<byte>();
			this.MessageId = 0;
		}

		public ServerMessage(uint Header)
		{
			this.Message = new List<byte>();
			this.MessageId = 0;
			this.Init(Header);
		}

		public void Boolean(bool b)
		{
			this.Bytes(new byte[] { b ? ((byte)1) : ((byte)0) }, false);
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

		public List<byte> BytesTo(byte[] b, bool IsInt)
		{
			List<byte> list = new List<byte>();
			if (IsInt)
			{
				for (int i = b.Length - 1; i > -1; i--)
				{
					list.Add(b[i]);
				}
				return list;
			}
			list.AddRange(b);
			return list;
		}

		public void Int(int i)
		{
			this.Bytes(BitConverter.GetBytes(i), true);
		}

		public void Short(int i)
		{
			short num = (short)i;
			this.Bytes(BitConverter.GetBytes(num), true);
		}

		public void String(string s)
		{
			this.Short(s.Length);
			this.Bytes(Encoding.UTF8.GetBytes(s), false);
		}

		public void UInt(uint i)
		{
			this.Int((int)i);
		}

		public byte[] ByteBuffer()
		{
			List<byte> list = new List<byte>();
			list.AddRange(BitConverter.GetBytes(this.Message.Count));
			list.Reverse();
			list.AddRange(this.Message);
			return list.ToArray();
		}

		public void Init(uint Header)
		{
			this.Message = new List<byte>();
			this.MessageId = (int)Header;
			this.Short((int)Header);
		}

		public void SetInt(int i, int startOn)
		{
			try
			{
				List<byte> message = new List<byte>();
				message = this.Message;
				List<byte> collection = this.BytesTo(BitConverter.GetBytes(i), true);
				message.RemoveRange(startOn, collection.Count);
				message.InsertRange(startOn, collection);
				this.Message = message;
			}
			catch (Exception exception)
			{
				Logging.Error("Can't set the Int!", exception, "ServerMessage", "SetInt");
			}
		}

		public override string ToString()
		{
			string output = "";
			foreach (char chr in Encoding.UTF8.GetString(this.Message.ToArray()).ToCharArray())
			{
				if (chr < 31)
				{
					output += "[" + (int)chr + "]";
				}
				else
				{
					output += chr;
				}
			}
			return output;
		}

		public int Id
		{
			get
			{
				return this.MessageId;
			}
		}

		public void Dispose()
		{
			this.Message.Clear();
		}
	}
}
