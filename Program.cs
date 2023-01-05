using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using UtilityBotImproved.Configuration;
using UtilityBotImproved.Controllers;
using UtilityBotImproved.Services;

namespace UtilityBotImproved
{
    internal class Program
    {
        static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }
        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();

            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();

            services.AddSingleton<IStorage, MemoryStorage>();            

            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "5780500043:AAEco3QDOpuDg3_EQ0D_yDf3uEz2FGXyKEw"
            };
        }
    }
}