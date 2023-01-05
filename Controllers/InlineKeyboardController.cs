using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBotImproved.Services;

namespace UtilityBotImproved.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).FunctionСode = callbackQuery.Data;

            // Генерим информационное сообщение
            string functionText = callbackQuery.Data switch
            {
                "stringLength" => " Длина строки",
                "sumNumbersString" => " Сумма чисел в строке",                
                _ => String.Empty
            };
            
            Console.WriteLine($"Контроллер {GetType().Name} получил сигнал о нажатой кнопке \"{functionText}\".");

            string note = (functionText == " Длина строки") ? "Ведите любое колличество символов, для их подсчета..."
                :"Введите числа которые хотите сложить, через пробелы между ними...";

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Вы выбрали функцию - {functionText}.{Environment.NewLine}</b>" +
                $"Можно поменять в главном меню.{Environment.NewLine}{Environment.NewLine}{note}"
                , cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
