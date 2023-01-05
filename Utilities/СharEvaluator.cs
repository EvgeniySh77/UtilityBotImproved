using Telegram.Bot.Types;

namespace UtilityBotImproved.Utilities
{
    public static class СharEvaluator
    {
        public static string Calculation(Message message)
        {
            return $"Колличество символов в строке: {message.Text.Length}";
        }
    }
}
