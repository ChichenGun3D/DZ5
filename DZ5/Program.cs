using System;

class Program
{
    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static double[] AverageByMonth(int[,] temperature)
    {
        int months = temperature.GetLength(0);
        int days = temperature.GetLength(1);

        double[] averages = new double[months];

        for (int i = 0; i < months; i++)
        {
            int sum = 0;
            for (int j = 0; j < days; j++)
            {
                sum += temperature[i, j];
            }
            averages[i] = (double)sum / days;
        }

        return averages;
    }

    static void SortAscending(double[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    double temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    static void Main(string[] args)
    {
        int[,] temperature = new int[12, 30];
        Random rnd = new Random();

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                temperature[i, j] = rnd.Next(-30, 36);
            }
        }

        Console.WriteLine("Температуры по месяцам:");
        PrintMatrix(temperature);

        double[] averages = AverageByMonth(temperature);

        Console.WriteLine("\nСредние температуры по месяцам:");
        for (int i = 0; i < averages.Length; i++)
        {
            Console.WriteLine($"Месяц {i + 1}: {averages[i]:F2}");
        }

        SortAscending(averages);

        Console.WriteLine("\nСредние температуры по возрастанию:");
        for (int i = 0; i < averages.Length; i++)
        {
            Console.WriteLine($"{averages[i]:F2}");
        }
    }
}