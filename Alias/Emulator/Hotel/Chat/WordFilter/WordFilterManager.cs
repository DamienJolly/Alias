using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Alias.Emulator.Hotel.Chat.WordFilter
{
    sealed class WordFilterManager
    {
		private List<WordFilterData> _swearWords;

		public WordFilterManager()
		{
			this._swearWords = new List<WordFilterData>();
		}

		public void Initialize()
		{
			if (this._swearWords.Count > 0)
			{
				this._swearWords.Clear();
			}

			this._swearWords = WordFilterDatabase.ReadSwearWords();
		}

		public string Filter(string message)
		{
			this._swearWords.ForEach(data =>
			{
				message = Regex.Replace(message, data.Phrase, Constant.FilterWord, RegexOptions.IgnoreCase);
			});
			return message;
		}

		public bool CheckBanned(string message)
		{
			return this._swearWords.Where(data => message.Contains(data.Phrase) && data.Bannable).Count() > 0;
		}
	}
}
