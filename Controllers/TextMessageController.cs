using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBotImproved.Services;
using UtilityBotImproved.Utilities;

namespace UtilityBotImproved.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение {message.Text}");

            string session = _memoryStorage.GetSession(message.Chat.Id).FunctionСode;
            if (message.Text == "/start")
            {
                //Объект, представляющий кноки
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                            InlineKeyboardButton.WithCallbackData($" Длина строки" , $"stringLength"),
                            InlineKeyboardButton.WithCallbackData($" Сумма чисел в строке" , $"sumNumbersString")
                        });

                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                    $"<b>  Этот бот подсчитывает длину строки или сумму чисел в строке.</b> {Environment.NewLine}" +
                    $"{Environment.NewLine}Для подсчета длины строки нажмите кнопку \"Длина строки\"," +
                    $" а для подсчета суммы чисел в строке нажмите кнопку \"Сумма чисел в строке\".{Environment.NewLine}",
                    cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
            }
            else if (message.Text != string.Empty && (session == "stringLength" || session == "sumNumbersString"))
            {
                string resultText = session switch
                {
                    "stringLength" => СharEvaluator.Calculation(message),
                    "sumNumbersString" => NumberAdder.Calculation(message),
                    _ => String.Empty
                };
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"{resultText}.{Environment.NewLine}");
            }
            else
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Вы забыли нажать на кнопку.",
                        cancellationToken: ct);            
        }
    }
}
