using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Menu_Example
{
    public class InlineButtonItem
    {
        private readonly string _text;
        private readonly string _data;

        public InlineKeyboardButton[] KeyBoardButton => new[]
        {
            InlineKeyboardButton.WithCallbackData(_text, _data)
        };

        public InlineButtonItem(string text, string data)
        {
            _text = text;
            _data = data;
        }
    }
}
