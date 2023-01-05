using Telegram.Bot.Types;

namespace UtilityBotImproved.Utilities
{
    public static class NumberAdder
    {

        public static string Calculation(Message message)
        {
            string resultSum;            
            var mergedSymbols = message.Text.Replace(" ","");
            bool @bool = int.TryParse(mergedSymbols, out _);

            if (@bool)
            {
                string[] arrayString = message.Text.Split(" ");
                int sum = 0;                
                foreach (var arrayNumbers in arrayString)
                {
                    sum += int.Parse(arrayNumbers);
                }                
                resultSum = sum.ToString();
                return $"Сумма чисел, которые вы ввели: {resultSum}";
            }           
            
            resultSum = "Вводите только числа, которые хотите сложить разделенные пробелами!";
            return resultSum;
            
        }
    }
}
