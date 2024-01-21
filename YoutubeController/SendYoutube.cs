using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;

namespace InstagramAndYoutube.YoutubeController
{
    public static class SendYoutube
    {
        public static async Task EssentialFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            string linkY = update.Message.Text;
            RootYoutube YoutubeVideoDownload = JsonConvert.DeserializeObject<RootYoutube>(YoutubeClass.RunApi(linkY).Result);

            await botClient.SendChatActionAsync(
                chatId: update.Message.Chat.Id,
                chatAction: ChatAction.UploadDocument,
                cancellationToken: cancellationToken);


            await botClient.SendVideoAsync(
                       chatId: update.Message.Chat.Id,
                       video: InputFileUrl.FromUri(YoutubeVideoDownload.formats[0].url),
                       supportsStreaming: true,
                       cancellationToken: cancellationToken);
        }
    }
}
