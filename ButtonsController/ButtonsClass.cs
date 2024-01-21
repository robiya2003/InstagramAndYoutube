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
    }
}
