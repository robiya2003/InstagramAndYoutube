using InstagramAndYoutube.ButtonsController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using InstagramAndYoutube.YoutubeController;
using Newtonsoft.Json;

namespace InstagramAndYoutube.CallBackQueryController
{
    public class CallBackQueryDataClass
    {
        public static async Task CheckFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
              var  ch = await botClient.GetChatMemberAsync("@mychanel12345678910", update.CallbackQuery.From.Id);
            

            if (ch.Status.ToString() == null || ch.Status.ToString() == "null" || ch.Status.ToString() == "Left" || ch.Status.ToString() == "")
            {
                ButtonsClass.CheckingSubscribe(botClient, update, cancellationToken);
                return;
            }
            else
            {
                if(update.CallbackQuery.Data.ToString()=="check")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: "Assalomu Alekum Kanalga obuna bo'ldingiz \n Botdan foydalanishingiz mumkin",
                        cancellationToken: cancellationToken);
                }
                else
                {
                    FindVideoFormat(botClient, update, cancellationToken);
                }
                
            }

        }
        public static async Task FindVideoFormat(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string[] IndexAndUrl= update.CallbackQuery.Data.ToString().Split(',');

            Console.WriteLine(update.CallbackQuery.Data.ToString());

            SendYoutube.EssentialFunction(botClient,update, cancellationToken, IndexAndUrl[1],int.Parse( IndexAndUrl[0]));
        }
    }
}
