// Learn more about F# at http://fsharp.org

open System
open Telegram.Bot
open Telegram.Bot.Args
open Telegram.Bot.Types

let Random = Random()

let Compliments: string list = [
    "blyo";
    "))))";
    "lol";
    "hahah";
    ":D";
    ":DDD"
]

let botClient: TelegramBotClient = TelegramBotClient ""

let Bot_OnMessage = EventHandler<MessageEventArgs> (fun (sender: obj) (e: MessageEventArgs) -> 
    if (e.Message.Photo <> null ||
        e.Message.Animation <> null ||
        e.Message.Video <> null) then
        
        let chosenCompliment = Compliments.[Random.Next(Compliments.Length)]

        botClient.SendTextMessageAsync(
            chatId = ChatId(e.Message.Chat.Id),
            text =  chosenCompliment + "from f#",
            replyToMessageId = e.Message.MessageId)
        |> Async.AwaitTask |> Async.RunSynchronously |> ignore)

[<EntryPoint>]
let main argv =
    
    botClient.OnMessage.AddHandler Bot_OnMessage
    botClient.StartReceiving()
    
    while true do
        ignore()
        
    0

        