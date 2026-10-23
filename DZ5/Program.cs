using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<string, double> AverageByMonth(Dictionary<string, int[]> temperature)
    {
        Dictionary<string, double> averages = new Dictionary<string, double>();

        foreach (KeyValuePair<string, int[]> pair in temperature)
        {
            int sum = 0;
            foreach (int t in pair.Value)
            {
                sum += t;
            }
            averages[pair.Key] = (double)sum / pair.Value.Length;
        }

        return averages;
    }

    static void Main(string[] args)
    {
        string[] months =
        {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        Dictionary<string, int[]> temperature = new Dictionary<string, int[]>();
        Random rnd = new Random();

        foreach (string month in months)
        {
            int[] days = new int[30];
            for (int i = 0; i < 30; i++)
            {
                days[i] = rnd.Next(-30, 36);
            }
            temperature[month] = days;
        }

        foreach (KeyValuePair<string, int[]> pair in temperature)
        {
            Console.Write(pair.Key + ": ");
            foreach (int t in pair.Value)
            {
                Console.Write(t + " ");
            }
            Console.WriteLine();
        }

        Dictionary<string, double> averages = AverageByMonth(temperature);

        Console.WriteLine("\nСредние температуры по месяцам:");
        foreach (KeyValuePair<string, double> pair in averages)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value:F2}");
        }

        List<KeyValuePair<string, double>> sorted = new List<KeyValuePair<string, double>>(averages);
        sorted.Sort((a, b) => a.Value.CompareTo(b.Value));

        Console.WriteLine("\nСредние температуры по возрастанию:");
        foreach (KeyValuePair<string, double> pair in sorted)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value:F2}");
        }
    }
}