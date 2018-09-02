using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Landing
{
    class LandingManager
    {
		private int _tick = 0;

		public Dictionary<int, LandingCompetition> Competitions
		{
			get; set;
		}

		public Dictionary<int, LandingBonusRare> BonusRares
		{
			get; set;
		}

		public List<LandingArticle> Articles
		{
			get; set;
		}

		public LandingManager()
		{
			this.Competitions = new Dictionary<int, LandingCompetition>();
			this.BonusRares = new Dictionary<int, LandingBonusRare>();
			this.Articles = new List<LandingArticle>();
		}

		public void Initialize()
		{
			this.Competitions = LandingDatabase.ReadCompetitions();
			this.BonusRares = LandingDatabase.ReadBonusRares();
			this.Articles = LandingDatabase.ReadArticles();
		}

		public void DoLandingCycle()
		{
			this._tick++;
			if (this._tick >= 1200)
			{
				Initialize();
			}
		}

		public bool TryGetCompetition(int id, out LandingCompetition data)
		{
			return this.Competitions.TryGetValue(id, out data);
		}

		public bool TryGetBonusRare(int id, out LandingBonusRare data)
		{
			return this.BonusRares.TryGetValue(id, out data);
		}
	}
}
