using System.Collections.Generic;
using Alias.Emulator.Utilities;

namespace Alias.Emulator.Hotel.Items
{
	class CrackableData
	{
		public int ItemId
		{
			get; set;
		}

		public Dictionary<int, int> Prizes
		{
			get; set;
		}

		public int Tick
		{
			get; set;
		}

		private int TotalChance
		{
			get; set;
		}

		public void LoadPrizes(string prizes)
		{
			this.Prizes = new Dictionary<int, int>();
			string[] data = prizes.Split(";");

			TotalChance = 0;
			for (int i = 0; i < data.Length; i++)
			{
				int itemId = 0;
				int chance = 100;

				if (data[i].Contains(":") && data[i].Split(":").Length == 2)
				{
					itemId = int.Parse(data[i].Split(":")[0]);
					chance = int.Parse(data[i].Split(":")[1]);
				}
				else
				{
					itemId = int.Parse(data[i].Replace(":", ""));
				}

				TotalChance += chance;
				this.Prizes.Add(itemId, TotalChance);
			}
		}

		public int GetRandomReward()
		{
			int random = Randomness.RandomNumber(TotalChance);

			int itemId = 0;
			foreach (KeyValuePair<int, int> prize in this.Prizes)
			{
				if (random >= prize.Value && random <= prize.Value)
				{
					return prize.Key;
				}
			}
			return itemId;
		}
	}
}
