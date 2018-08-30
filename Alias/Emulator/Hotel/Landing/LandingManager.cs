using System.Collections.Generic;

namespace Alias.Emulator.Hotel.Landing
{
    class LandingManager
    {
		private int _tick = 0;

		public Dictionary<string, LandingCompetition> Competitions
		{
			get; set;
		}

		public Dictionary<string, LandingBonusRare> BonusRares
		{
			get; set;
		}

		public List<LandingArticle> Articles
		{
			get; set;
		}

		public LandingManager()
		{
			this.Competitions = new Dictionary<string, LandingCompetition>();
			this.BonusRares = new Dictionary<string, LandingBonusRare>();
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

		public bool TryGetCompetition(string name, out LandingCompetition data)
		{
			return this.Competitions.TryGetValue(name, out data);
		}

		public bool TryGetBonusRare(string name, out LandingBonusRare data)
		{
			return this.BonusRares.TryGetValue(name, out data);
		}
	}
}
