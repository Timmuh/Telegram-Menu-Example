using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Menu_Example
{
    public class InlineButtonsMenu
    {
        public List<InlineButtonItem> InlineButtonItems { get; set; }
        public int MaxCount { get; set; } = 3;

        private readonly TelegramBotClient _bot;

        private List<List<InlineKeyboardButton[]>> _seiten;

        public string NextText { get; set; } = ">> Next >>";
        public string BackText { get; set; } = "<< Back <<";

        public InlineKeyboardMarkup Markup
        {
            get
            {
                var list = new List<InlineKeyboardButton[]>();
                if (InlineButtonItems.Count < MaxCount)
                {
                    foreach (var inlineButtonItem in InlineButtonItems)
                    {
                        list.Add(inlineButtonItem.KeyBoardButton);

                    }
                    return new InlineKeyboardMarkup(list.ToArray());
                }
                else
                {
                    var foo = (double)InlineButtonItems.Count / MaxCount;
                    var seitenCount = Math.Ceiling(foo);
                    var seiten = new List<List<InlineKeyboardButton[]>>();
                    var nextItem = 0;
                    var rest = InlineButtonItems.Count;

                    for (int i = 0; i < seitenCount; i++)
                    {
                        seiten.Add(new List<InlineKeyboardButton[]>());
                        for (int j = 0; j <= MaxCount - 1; j++)
                        {
                            if (rest == 0) break;
                            seiten[i].Add(InlineButtonItems[nextItem].KeyBoardButton);
                            nextItem++;
                            rest--;
                        }

                        var z = InlineKeyboardButton.WithCallbackData(BackText, $"InlineMenuAction;{this.GetHashCode()};Back;{i}");
                        var w = InlineKeyboardButton.WithCallbackData(NextText, $"InlineMenuAction;{this.GetHashCode()};Next;{i}");

                        if (seiten.Count != 1 && seiten.Count != seitenCount)
                        {
                            seiten[i].Add(new[] { z, w });
                        }
                        else
                        {
                            if (seiten.Count != 1)
                            {
                                seiten[i].Add(new[] { z });
                            }

                            if (seiten.Count != seitenCount)
                            {
                                seiten[i].Add(new[] { w });
                            }
                        }

                    }
                    _seiten = seiten;
                    return new InlineKeyboardMarkup(seiten[0].ToArray());
                }
            }
        }

        public InlineButtonsMenu(List<InlineButtonItem> items, TelegramBotClient bot) : this(bot)
        {
            InlineButtonItems = items;
        }

        public InlineButtonsMenu(TelegramBotClient bot)
        {
            InlineButtonItems = new List<InlineButtonItem>();
            _bot = bot;
            _bot.OnCallbackQuery += BotOnOnCallbackQuery;
        }

        private void BotOnOnCallbackQuery(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var args = callbackQueryEventArgs.CallbackQuery.Data.Split(';');
            if (args.Length == 4)
            {
                if (args[0] == "InlineMenuAction")
                {
                    if (args[1] == this.GetHashCode().ToString())
                    {
                        if (args[2] == "Next")
                        {
                            var currentPage = Convert.ToInt32(args[3]);
                            var msg = callbackQueryEventArgs.CallbackQuery.Message;
                            _bot.EditMessageReplyMarkupAsync(msg.Chat.Id, msg.MessageId, new InlineKeyboardMarkup(_seiten[currentPage + 1].ToArray()));
                        }
                        else if (args[2] == "Back")
                        {
                            var currentPage = Convert.ToInt32(args[3]);
                            var msg = callbackQueryEventArgs.CallbackQuery.Message;
                            _bot.EditMessageReplyMarkupAsync(msg.Chat.Id, msg.MessageId, new InlineKeyboardMarkup(_seiten[currentPage - 1].ToArray()));
                        }
                    }
                }
            }
        }
    }
}
