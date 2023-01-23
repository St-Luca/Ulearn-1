using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            int daysNumbers = 31;
            string[] arrayOfDays = new string[daysNumbers];

            //Заполнение массива подписей по оси Х
            for (int i = 0; i < daysNumbers; i++)
            {
                arrayOfDays[i] = (i + 1).ToString();
            }

            double[] birthsNumbers = new double[daysNumbers];

            //В цикле перебираем имена и откидываем рожденных 1 числа
            foreach (var human in names)
            {
                if (human.BirthDate.Day != 1 && human.Name == name)
                {
                    birthsNumbers[human.BirthDate.Day - 1]++;
                }
            }

            return new HistogramData(string.Format("Рождаемость людей с именем '{0}'", name),
                arrayOfDays, birthsNumbers);
        }
    }
}