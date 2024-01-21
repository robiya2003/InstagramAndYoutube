using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using InstagramAndYoutube.MessageController;
using InstagramAndYoutube.OtherController;
using InstagramAndYoutube.ButtonsController;
using InstagramAndYoutube.CallBackQueryController;

namespace InstagramAndYoutube.ServiseController
{
    public static class ServiseClass
    {
        public static async Task EssentialControlFunction()
        {
            #region
            var botClient = new TelegramBotClient("6883397115:AAFP8SjHbupFchEx3GdK_-W7_i0LSAwuutU");
            using CancellationTokenSource cts = new();
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cts.Cancel();
            #endregion
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                var handler = update.Type switch
                {
                    UpdateType.Message => MessageClass.EssentialFunction(botClient, update, cancellationToken),
                    UpdateType.CallbackQuery=> CallBackQueryDataClass.CheckFunction(botClient, update, cancellationToken),
                    _ =>OtherUpdateClass.EssentialFunction(botClient,update, cancellationToken)
                };
                try
                {
                    await handler;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

            }
            #region
            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }
            #endregion
        }
    }
}