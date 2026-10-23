using System;

class Program
{
    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static int[,] InputMatrix(int rows, int cols, string name)
    {
        int[,] matrix = new int[rows, cols];

        Console.WriteLine($"Введите элементы матрицы {name} ({rows} строк, {cols} столбцов):");

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{name}[{i},{j}] = ");
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }

        return matrix;
    }

    static int[,] MultiplyMatrices(int[,] A, int[,] B)
    {
        int rowsA = A.GetLength(0);
        int colsA = A.GetLength(1);
        int colsB = B.GetLength(1);

        int[,] C = new int[rowsA, colsB];

        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                int sum = 0;
                for (int k = 0; k < colsA; k++)
                {
                    sum = sum + A[i, k] * B[k, j];
                }
                C[i, j] = sum;
            }
        }

        return C;
    }

    static void Main(string[] args)
    {
        Console.Write("Сколько строк в матрице A? ");
        int rowsA = int.Parse(Console.ReadLine());

        Console.Write("Сколько столбцов в матрице A? ");
        int colsA = int.Parse(Console.ReadLine());

        Console.Write("Сколько строк в матрице B? ");
        int rowsB = int.Parse(Console.ReadLine());

        Console.Write("Сколько столбцов в матрице B? ");
        int colsB = int.Parse(Console.ReadLine());

        if (colsA != rowsB)
        {
            Console.WriteLine("Ошибка: матрицы нельзя умножить!");
            Console.WriteLine($"Столбцов в A = {colsA}, строк в B = {rowsB}. Они должны быть равны.");
            return;
        }

        int[,] A = InputMatrix(rowsA, colsA, "A");
        int[,] B = InputMatrix(rowsB, colsB, "B");

        Console.WriteLine("\nМатрица A:");
        PrintMatrix(A);

        Console.WriteLine("\nМатрица B:");
        PrintMatrix(B);

        int[,] C = MultiplyMatrices(A, B);

        Console.WriteLine("\nРезультат A × B:");
        PrintMatrix(C);
    }
}
