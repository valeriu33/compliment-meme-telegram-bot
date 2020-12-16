using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ComplimentMemeTelegramBot
{
    class Program
    {
        private static ITelegramBotClient botClient;

        private static readonly Random Random = new Random();
        
        private static readonly string[] Compliments = {
            "That's a really nice meme, bruh",
            "I actually laughed at this one :D",
            "Good one!",
            "I lol'd",
            "Nice meme, buddy",
            ":DDD"
        };
        
        public static async Task Main(string[] args)
        {
            botClient = new TelegramBotClient("");
            var me = await botClient.GetMeAsync();
            
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            botClient.StopReceiving();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Photo == null &&
                e.Message.Animation == null &&
                e.Message.Video == null) return;

            var chosenCompliment = Compliments[Random.Next(Compliments.Length)];

            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text:   chosenCompliment,
                replyToMessageId: e.Message.MessageId
            );
        }
    }
}
