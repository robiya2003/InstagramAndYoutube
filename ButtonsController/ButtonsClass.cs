using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using InstagramAndYoutube.YoutubeController;
using Newtonsoft.Json;
using System.Web;

namespace InstagramAndYoutube.ButtonsController
{
    public class ButtonsClass
    {
        public static async Task CheckingSubscribe(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
            {
                InlineKeyboardButton.WithUrl(
                    text: "Kanalga o'tish",
                    url: "https://t.me/mychanel12345678910"),
                InlineKeyboardButton.WithCallbackData(text:"Tekshirish",callbackData:"check")
            });

            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Kanalga obuna bo'ling",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }
        public static async Task AdaptiveFornats(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string linkY = update.Message.Text;
            string url = "";
            var uri = new Uri(linkY);
            var query = HttpUtility.ParseQueryString(uri.Query);
            if (query.AllKeys.Contains("v"))
            {
                url = query["v"];
            }
            else
            {
                url = uri.Segments.Last();
            }


            RootYoutube YoutubeVideoFormat = JsonConvert.DeserializeObject<RootYoutube>(YoutubeClass.RunApi(url).Result);
            var formats = YoutubeVideoFormat.formats;
            var buttons = new List<List<InlineKeyboardButton>>();
            var button = new List<InlineKeyboardButton>();

            for (int i=0;i<formats.Count;i++)
            {
                if (button.Count < 2 && formats[i].qualityLabel != null) 
                {
                    button.Add(InlineKeyboardButton.WithCallbackData(text: formats[i].qualityLabel+" "+ formats[i].quality, 
                        callbackData: i.ToString()+","+url));
                }
                else
                {
                    if (formats[i].qualityLabel != null)
                    {
                        buttons.Add(button);
                        button = new List<InlineKeyboardButton> { InlineKeyboardButton.WithCallbackData(text: formats[i].qualityLabel + " " + formats[i].quality,
                            callbackData: i.ToString()+","+url)};
                    }
                }
            }
            if (button.Count > 0)
            {
                buttons.Add(button);
            }
            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Video Formatini  tanlang",
                replyMarkup: new InlineKeyboardMarkup(buttons),
                cancellationToken: cancellationToken);
        }
    }
}
