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

		public LandingManager()
		{
			this.Competitions = new Dictionary<string, LandingCompetition>();
		}

		public void Initialize()
		{
			this.Competitions = LandingDatabase.ReadCompetitions();
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
    }
}
