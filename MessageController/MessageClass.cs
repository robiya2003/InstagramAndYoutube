using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using InstagramAndYoutube.ButtonsController;
using Telegram.Bot.Types.Enums;
using Newtonsoft.Json;
using InstagramAndYoutube.InstagramController;
using InstagramAndYoutube.YoutubeController;

namespace InstagramAndYoutube.MessageController
{
    public static class MessageClass
    {
        public static async Task EssentialFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
             var  ch = await botClient.GetChatMemberAsync("@mychanel12345678910", update.Message.From.Id);
            if (ch.Status.ToString() == null || ch.Status.ToString() == "null" || ch.Status.ToString() == "Left" || ch.Status.ToString() == "")
            {
                ButtonsClass.CheckingSubscribe(botClient, update, cancellationToken);
                return;
            }
            if(MessageType.Text==update.Message.Type)
            {
                await TextFunction(botClient,update,cancellationToken);
            }
            else
            {
                return;
            }
            
            
             
        }
        public static async Task TextFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message=update.Message.Text;
            if (message == "/start")
            {
                await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Assalomu Alekum Botga hush kelibsiz",
                    cancellationToken: cancellationToken);
            }
            else if (update.Message.Text.StartsWith("https://www.instagram.com"))
            {
                await SendInstagramClass.EssentialFunction(botClient, update, cancellationToken);
            }
            else if (message.StartsWith("https://www.youtube.com") || message.StartsWith("https://youtu.be"))
            {
                SendYoutube.EssentialFunction(botClient,update, cancellationToken);
            }
        }
    }
}
