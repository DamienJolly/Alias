using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alias.Emulator.Hotel.Rooms.Entities
{
	public class UserActions
	{
		private Dictionary<string, string> actions
		{
			get; set;
		} = new Dictionary<string, string>();

		public void Add(string key, string value)
		{
			this.Remove(key);
			this.actions.Add(key, value);
		}

		public void Remove(string key)
		{
			this.actions.Remove(key);
		}

		public void Clear()
		{
			this.actions.Clear();
		}

		public bool Has(string key)
		{
			return this.actions.ContainsKey(key);
		}

		public string String
		{
			get
			{
				StringBuilder builder = new StringBuilder();
				builder.Append("/");
				this.actions.ToList().ForEach(kvp =>
				{
					builder.Append(kvp.Key);
					if (!string.IsNullOrEmpty(kvp.Value))
					{
						builder.Append(" ");
						builder.Append(kvp.Value);
					}
					builder.Append("/");
				});
				builder.Append("/");
				return builder.ToString();
			}
		}
	}
}
