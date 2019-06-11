using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Telegram_Menu_Example
{
    internal class Bot
    {
        public Bot()
        {
            Init();
        }

        public static TelegramBot TeleBot { get; set; }

        private async void Init()
        {
            TeleBot = new TelegramBot();
            TeleBot.Activate();
            Console.Title = $"Telegram MRE Example | TelegramBot {TelegramBot.TeleBot.GetMeAsync().Result.FirstName} (ID:{TelegramBot.TeleBot.GetMeAsync().Result.Id})";
        }

        public void Run()
        {
            while (true)
            {
                var msg = Console.ReadLine();
                if (msg == "quit") Environment.Exit(0);
            }
        }
    }
}
