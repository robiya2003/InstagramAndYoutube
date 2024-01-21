using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;
using InstagramAndYoutube.ButtonsController;
using System.Web;

namespace InstagramAndYoutube.YoutubeController
{
    public static class SendYoutube
    {
        public static async Task EssentialFunction(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken,string url,int index)
        {

            RootYoutube YoutubeVideoDownload = JsonConvert.DeserializeObject<RootYoutube>(YoutubeClass.RunApi(url).Result);

            await botClient.SendChatActionAsync(
                chatId: update.CallbackQuery.From.Id,
                chatAction: ChatAction.UploadDocument,
                cancellationToken: cancellationToken);


            await botClient.SendVideoAsync(
                       chatId: update.CallbackQuery.From.Id,
                       video: InputFileUrl.FromUri(YoutubeVideoDownload.formats[index].url),
                       caption:"Formati : "+ YoutubeVideoDownload.formats[index].qualityLabel+"\ntitle : "+YoutubeVideoDownload.title+"\nSeconds : "+YoutubeVideoDownload.lengthSeconds,
                       supportsStreaming: true,
                       cancellationToken: cancellationToken);
        }
    }
}
