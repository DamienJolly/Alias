using System.Text;
using DotNetty.Buffers;

namespace Alias.Emulator.Network.Protocol
{
	public class ClientMessage
	{
		private IByteBuffer Buffer;
		public readonly int Id;

		public ClientMessage(IByteBuffer buffer)
		{
			this.Buffer = buffer;
			this.Id = this.Short();
		}

		public int Integer()
		{
			return this.Buffer.ReadInt();
		}

		public short Short()
		{
			return this.Buffer.ReadShort();
		}

		public string String()
		{
			short length = this.Short();
			byte[] buffer = new byte[length];
			this.Buffer.ReadBytes(buffer);
			return Encoding.UTF8.GetString(buffer);
		}

		public bool Boolean()
		{
			return this.Buffer.ReadByte() == 1;
		}

		public override string ToString()
		{
			string output = "";
			foreach (char chr in this.Buffer.ToString(Encoding.UTF8).ToCharArray())
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

		public void Dispose()
		{
			this.Buffer.Clear();
			this.Buffer = null;
		}
	}

}
