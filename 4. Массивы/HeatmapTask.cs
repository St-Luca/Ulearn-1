using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {  //С помощью методов создаем и заполняем массивы для данных 
            string[] monthes = GetMonthNumber(names);
            string[] days = GetDayNumber(names);

            double[,] heatmap = new double[30, 12];
            //В цикле перебираем имена и откидываем рожденных 1 числа
            foreach (var name in names)
                if (name.BirthDate.Day != 1)
                    heatmap[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
            return new HeatmapData("Пример карты интенсивностей", heatmap, days, monthes);
        }

        //Метод для заполнения массива дней
        public static string[] GetDayNumber(NameData[] names)
        {
            var arrayOfDayNumber = new string[30];
            for (int i = 0; i < 30; i++)
            {
                arrayOfDayNumber[i] = (i + 2).ToString();
            }
            return arrayOfDayNumber;
        }

        //Метод для заполнения массива месяцев
        public static string[] GetMonthNumber(NameData[] names)
        {
            var arrayOfMonthesNumber = new string[12];
            for (int i = 0; i < 12; i++)
            {
                arrayOfMonthesNumber[i] = (i + 1).ToString();
            }
            return arrayOfMonthesNumber;
        }
    }
}