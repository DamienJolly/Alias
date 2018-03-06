using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Alias.Emulator.Hotel.Misc.WordFilter
{
    public class WordFilterManager
    {
		private static List<WordFilterData> SwearWords;

		public static void Initialize()
		{
			WordFilterManager.SwearWords = WordFilterDatabase.ReadSwearWords();
		}

		public static string Filter(string message)
		{
			WordFilterManager.SwearWords.ForEach(data =>
			{
				message = Regex.Replace(message, data.Phrase, Constant.FilterWord, RegexOptions.IgnoreCase);
			});
			return message;
		}

		public static bool CheckBanned(string message)
		{
			return WordFilterManager.SwearWords.Where(data => message.Contains(data.Phrase) && data.Bannable).Count() > 0;
		}
	}
}
