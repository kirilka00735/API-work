using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WebApplication1.Models;

namespace Bot
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var botClient = new TelegramBotClient("6887511983:AAGIkqvdXL7mMwH9vJtQPo5z4-7oT9-J0eg");
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
            Console.WriteLine($"Начинаю слушать @{me.Username}");
            HttpClient client = new HttpClient();
            var result = await client.GetAsync("https://localhost:7045/api/Photo");
            Console.WriteLine(result);
            var test = await result.Content.ReadAsStringAsync();
            Console.WriteLine(test);
            Photo[] photos = JsonConvert.DeserializeObject<Photo[]>(test);
            foreach (var photo in photos)
                Console.WriteLine(photo.PhotoId + " ," + photo.PlaceId + " ," + photo.UserId + " ," + photo.PhotoUrl + " ," + photo.Description + ".");


            Console.ReadLine();
            cts.Cancel();
        }
        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Получено текстовое сообщение '{messageText}' сообщение в чате {chatId}.");

            if (messageText.Equals("Привет", StringComparison.OrdinalIgnoreCase) || messageText.Equals("Здравствуй", StringComparison.OrdinalIgnoreCase))
            {
                string userNickname = message.From.Username;
                string greetingResponse = $"Здравствуй, {userNickname}!";
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: greetingResponse,
                    cancellationToken: cancellationToken
                );
            }
            else if (messageText.Equals("Картинка", StringComparison.OrdinalIgnoreCase))
            {
                string imageUrl = "https://i-a.d-cd.net/bsAAAgGWkuA-1920.jpg";
                Message sentMessage = await botClient.SendPhotoAsync(
                    chatId: chatId,
                    photo: InputFile.FromUri("https://i-a.d-cd.net/bsAAAgGWkuA-1920.jpg"),
                    caption: "Вот ваша картинка!",
                    cancellationToken: cancellationToken
                );
            }
            else if (messageText.Equals("Видео", StringComparison.OrdinalIgnoreCase))
            {
                string videoUrl = "https://www.example.com/path/to/your/video.mp4";
                Message sentMessage = await botClient.SendVideoAsync(
                    chatId: chatId,
                    video: InputFile.FromUri("https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4"),
                    thumbnail: InputFile.FromUri("https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg"),
                    supportsStreaming: true,
                    cancellationToken: cancellationToken
                );
            }
            else if (messageText.Equals("Стикер", StringComparison.OrdinalIgnoreCase))
            {
                Message sentMessage1 = await botClient.SendStickerAsync(
                    chatId: chatId,
                    sticker: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"),
                    cancellationToken: cancellationToken
                );
            }
            else if (messageText.Equals("Кнопки", StringComparison.OrdinalIgnoreCase))
            {
                var keyboard = new ReplyKeyboardMarkup(new[]
                {
            new[]
            {
                new KeyboardButton("Ответ 1"),
                new KeyboardButton("Ответ 2")
            }
        });
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Выберите один из вариантов:",
                    replyMarkup: keyboard,
                    cancellationToken: cancellationToken
                );
            }
            else
            {
                string responseText = "Вы сказали:\n" + messageText;
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: responseText,
                    cancellationToken: cancellationToken);
            }
        }

        static async Task HandleState(ITelegramBotClient botClient, Message message, string currentState, CancellationToken cancellationToken)
        {
            
        }

        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API error: \n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString(),
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
