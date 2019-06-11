using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Timer = System.Timers.Timer;

namespace Telegram_Menu_Example
{
    internal class TelegramBot
    {
        public static List<long> UserIdsAsked = new List<long>();

        public TelegramBot()
        {
            var token = "YOUR TOKEN";

            if (!string.IsNullOrEmpty(token))
            {
                TeleBot = new TelegramBotClient(token);
                TeleBot.OnUpdate += OnUpdate;
            }
            else
            {
                throw new NotImplementedException("No Token in TelegramBot.cs!");
            }
        }

        public static TelegramBotClient TeleBot { get; set; }

        public void Activate()
        {
            TeleBot.StartReceiving();
        }

        public void Deactivate()
        {
            TeleBot.StopReceiving();
        }

        public static void OnUpdate(object sender, UpdateEventArgs e)
        {
            if (e.Update.Type != UpdateType.Message || e.Update.Message.Type != MessageType.Text) return;
            TelegramCommands.ManageTelegramMessage(e);
        }
    }
}
