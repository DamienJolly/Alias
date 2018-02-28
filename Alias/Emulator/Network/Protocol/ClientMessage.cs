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

		public void Dispose()
		{
			this.Buffer.Clear();
			this.Buffer = null;
		}
	}
}
