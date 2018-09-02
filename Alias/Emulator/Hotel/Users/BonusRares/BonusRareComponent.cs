using System.Collections.Generic;
using Alias.Emulator.Hotel.Items;

namespace Alias.Emulator.Hotel.Users.BonusRares
{
    class BonusRareComponent
    {
		private Habbo habbo;

		public Dictionary<int, int> BonusRares
		{
			get; set;
		}

		public BonusRareComponent(Habbo h)
		{
			this.habbo = h;
			this.BonusRares = BonusRaresDatabase.ReadBonusRares(h.Id);
		}
		
		public int GetProgress(int id)
		{
			if (!this.BonusRares.TryGetValue(id, out int progress))
			{
				progress = 0;
				AddBonusRare(id, progress);
			}
			return progress;
		}

		public void AddBonusRare(int id, int progress)
		{
			if (!HasBonusRare(id))
			{
				this.BonusRares.Add(id, progress);
				BonusRaresDatabase.AddBonusRare(this.habbo.Id, id, progress);
			}
		}

		public void UpdateBonusRare(int id, int progress)
		{
			if (HasBonusRare(id))
			{
				this.BonusRares[id] += progress;
				BonusRaresDatabase.UpdateBonusRare(this.habbo.Id, id, progress);
			}
		}

		public void GivePrize(int id, ItemData prize)
		{
			//todo:
		}

		public bool HasBonusRare(int id) => this.BonusRares.ContainsKey(id);
	}
}
