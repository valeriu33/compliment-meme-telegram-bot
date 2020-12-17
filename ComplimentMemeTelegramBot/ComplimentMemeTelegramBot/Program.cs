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
        
        private static readonly string[] Offends = {
            "Huinea memu, bratan",
            "mhm..",
            "meh..",
            "fu, Misha",
            "Tacoi sebe mem",
            ":DDD"
        };
        
        public static async Task Main(string[] args)
        {
            var compliment_meme_id = "";
            botClient = new TelegramBotClient(compliment_meme_id);
            var me = await botClient.GetMeAsync();
            
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            
            while (true) { } // To keep the app alive on the server :)
            // Console.ReadKey();
            botClient.StopReceiving();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Photo == null &&
                e.Message.Animation == null &&
                e.Message.Video == null) return;

            var chosenReply = Compliments[Random.Next(Compliments.Length)];

            if (e.Message.From?.FirstName == "Mihail")
            {
                chosenReply = Offends[Random.Next(Offends.Length)];
            }
            
            await botClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text:   chosenReply,
                replyToMessageId: e.Message.MessageId
            );
        }
    }
}
