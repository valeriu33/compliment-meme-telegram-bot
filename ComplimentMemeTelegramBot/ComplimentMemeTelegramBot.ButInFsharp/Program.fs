open System
open Telegram.Bot
open Telegram.Bot.Args
open Telegram.Bot.Types

let Random = Random()

let Compliments: string list = [
    "blyo";
    "))))";
    "lol";
    "hahahah";
    ":D";
    ":DDD"
]

let Offences: string list = [
    "Huinea memu, bratan";
    "mhm..";
    "meh..";
    "fu, Emil";
    "Tacoi sebe mem";
    ":DDD"
]

let keep_chat_alive_id = ""
let botClient = TelegramBotClient keep_chat_alive_id

let BotOnMessage = EventHandler<MessageEventArgs> (fun (sender: obj) (e: MessageEventArgs) -> 
    if ((not (isNull e.Message.Photo)) ||
        (not (isNull e.Message.Animation)) ||
        (not (isNull e.Message.Video))) then
        
        let mutable chosenReply = Compliments.[Random.Next(Compliments.Length)]
        
        if e.Message.From.FirstName = "Emil" then
            chosenReply <- Offences.[Random.Next(Offences.Length)]
        
        botClient.SendTextMessageAsync(
            chatId = ChatId(e.Message.Chat.Id),
            text =  chosenReply,
            replyToMessageId = e.Message.MessageId)
        |> Async.AwaitTask |> Async.RunSynchronously |> ignore)

[<EntryPoint>]
let main argv =
    botClient.OnMessage.AddHandler BotOnMessage
    botClient.StartReceiving()
    
    while true do
        ignore()
        
    0

        
