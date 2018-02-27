using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Users.Badges
{
    public class BadgeComponent
    {
		private List<BadgeDefinition> badges;
		private Habbo habbo;

		public BadgeComponent(Habbo h)
		{
			this.habbo = h;
			this.badges = Initialize(this.habbo.Id);
		}

		public static List<BadgeDefinition> Initialize(int userId)
		{
			return BadgeDatabase.InitBadges(userId);
		}

		public Habbo Habbo()
		{
			return this.habbo;
		}

		public List<BadgeDefinition> GetBadges()
		{
			return this.badges;
		}

		public BadgeDefinition GetBadge(string code)
		{
			return this.badges.Where(badge => badge.Code == code).FirstOrDefault();
		}

		public List<BadgeDefinition> GetWearingBadges()
		{
			return this.badges.Where(badge => badge.Slot > 0).OrderBy(badge => badge.Slot).ToList();
		}

		public void ResetSlots()
		{
			foreach (BadgeDefinition badge in this.GetWearingBadges())
			{
				badge.Slot = 0;
				BadgeDatabase.UpdateBadge(this.Habbo(), badge, badge.Code);
			}
		}

		public void UpdateBadge(BadgeDefinition badge, string code)
		{
			if (this.HasBadge(code))
			{
				BadgeDatabase.UpdateBadge(this.Habbo(), badge, code);
			}
		}

		public void GiveBadge(string code)
		{
			if (!this.HasBadge(code))
			{
				BadgeDatabase.GiveBadge(this.Habbo(), code);
				BadgeDefinition badge = new BadgeDefinition()
				{
					Code = code,
					Slot = 0
				};
				this.badges.Add(badge);
			}
		}

		public void TakeBadge(string code)
		{
			if (this.HasBadge(code))
			{
				BadgeDatabase.TakeBadge(this.Habbo(), code);
				this.badges.Remove(this.badges.Where(badge => badge.Code == code).First());
			}
		}

		public bool HasBadge(string code)
		{
			return this.badges.Where(badge => badge.Code == code).Count() > 0;
		}
		
		public void Dispose()
		{
			this.badges.Clear();
			this.habbo = null;
		}
	}
}
