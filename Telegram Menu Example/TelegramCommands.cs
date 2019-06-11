using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types;


namespace Telegram_Menu_Example
{
    class TelegramCommands
    {
        public static void ManageTelegramMessage(UpdateEventArgs e)
        {
            var user = e.Update.Message.From;
            var fromID = user.Id;
            var messageText = e.Update.Message.Text;
            var message = e.Update.Message;
            var botName = TelegramBot.TeleBot.GetMeAsync().Result.Username;
            var chatID = e.Update.Message.Chat.Id;
            var messageID = e.Update.Message.MessageId;

            WriteColoredLine($"[{DateTime.Now}] | {user.Username}: {messageText}", ConsoleColor.Cyan);

            if (TelegramBot.UserIdsAsked.Contains(fromID)) return;

            if (Command(messageText, "start"))
            {
                TelegramBot.TeleBot.SendTextMessageAsync(fromID, $"Hello {user.FirstName}. This is an example Bot for Menus.");
            }
            else
            {
                if (StandardCommands(e, messageText, chatID, user, fromID)) return;
            }
        }

        private static bool StandardCommands(UpdateEventArgs e, string messageText, long chatID, User user, int fromId)
        {
            if (Command(messageText, "menu", startswith: true))
            {
                InlineButtonsMenu set = new InlineButtonsMenu(TelegramBot.TeleBot);

                //Set Text for "Next" Button
                set.NextText = "Next »";

                //Set Text for "Back" Button
                set.BackText = "« Back";

                //Set Maximum Buttons per "Page"
                set.MaxCount = 5;

                set.InlineButtonItems.Add(new InlineButtonItem("Test 1", "test1"));
                set.InlineButtonItems.Add(new InlineButtonItem("Test 2", "test2"));
                set.InlineButtonItems.Add(new InlineButtonItem("Test 3", "test3"));
                set.InlineButtonItems.Add(new InlineButtonItem("Test 4", "test4"));
                set.InlineButtonItems.Add(new InlineButtonItem("Test 5", "test5"));
                set.InlineButtonItems.Add(new InlineButtonItem("Test 6", "test6"));
                set.InlineButtonItems.Add(new InlineButtonItem("Test 7", "test7"));
                set.InlineButtonItems.Add(new InlineButtonItem("Test 8", "test8"));
                set.InlineButtonItems.Add(new InlineButtonItem("Test 9", "test9"));
                set.InlineButtonItems.Add(new InlineButtonItem("Test 10", "test10"));

                TelegramBot.TeleBot.SendTextMessageAsync(chatID, "Example Menu", replyMarkup: set.Markup);
            }

            return false;
        }


        public static bool Command(string message, string command, bool toLower = true, bool startswith = false, bool allowInGroup = false)
        {
            if (toLower)
            {
                message = message.ToLower();
                command = command.ToLower();
            }
            if (!allowInGroup)
            {
                if (startswith)
                    return message.StartsWith($"/{command}");
                return message == $"/{command}";
            }
            var bot = TelegramBot.TeleBot.GetMeAsync().Result.Username.ToLower();
            if (startswith)
                return message.StartsWith($"/{command}") || message.StartsWith($"/{command}@{bot}");
            return message == $"/{command}" || message == $"/{command}@{bot}";
        }

        public static void WriteColoredLine(string text, ConsoleColor color)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = old;
        }

    }
}
