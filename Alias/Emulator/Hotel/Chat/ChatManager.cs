using Alias.Emulator.Hotel.Chat.Commands;
using Alias.Emulator.Hotel.Chat.Emotions;
using Alias.Emulator.Hotel.Chat.WordFilter;

namespace Alias.Emulator.Hotel.Chat
{
    sealed class ChatManager
    {
		private WordFilterManager _filter;
		private ChatEmotionsManager _emotions;
		private CommandManager _commands;

		public ChatManager()
		{
			this._filter = new WordFilterManager();
			this._emotions = new ChatEmotionsManager();
			this._commands = new CommandManager(":");
		}

		public void Initialize()
		{
			this._filter.Initialize();
		}

		public WordFilterManager GetFilter()
		{
			return this._filter;
		}

		public ChatEmotionsManager GetEmotions()
		{
			return this._emotions;
		}

		public CommandManager GetCommands()
		{
			return this._commands;
		}
	}
}
