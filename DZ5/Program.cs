using System;
using System.Collections.Generic;

class Program
{
    static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
    {
        foreach (LinkedList<int> row in matrix)
        {
            foreach (int value in row)
            {
                Console.Write(value + "\t");
            }
            Console.WriteLine();
        }
    }

    static LinkedList<LinkedList<int>> InputMatrix(int rows, int cols, string name)
    {
        LinkedList<LinkedList<int>> matrix = new LinkedList<LinkedList<int>>();

        Console.WriteLine($"Введите элементы матрицы {name} ({rows} строк, {cols} столбцов):");

        for (int i = 0; i < rows; i++)
        {
            LinkedList<int> row = new LinkedList<int>();
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{name}[{i},{j}] = ");
                row.AddLast(int.Parse(Console.ReadLine()));
            }
            matrix.AddLast(row);
        }

        return matrix;
    }

    static LinkedList<LinkedList<int>> MultiplyMatrices(LinkedList<LinkedList<int>> A, LinkedList<LinkedList<int>> B)
    {
        int rowsA = A.Count;
        int colsA = A.First.Value.Count;
        int colsB = B.First.Value.Count;

        int[,] B_arr = new int[B.Count, colsB];
        int bi = 0;
        foreach (LinkedList<int> row in B)
        {
            int bj = 0;
            foreach (int value in row)
            {
                B_arr[bi, bj] = value;
                bj++;
            }
            bi++;
        }

        LinkedList<LinkedList<int>> C = new LinkedList<LinkedList<int>>();

        foreach (LinkedList<int> rowA in A)
        {
            LinkedList<int> resultRow = new LinkedList<int>();

            for (int j = 0; j < colsB; j++)
            {
                int sum = 0;
                int k = 0;
                foreach (int valueA in rowA)
                {
                    sum += valueA * B_arr[k, j];
                    k++;
                }
                resultRow.AddLast(sum);
            }

            C.AddLast(resultRow);
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

        LinkedList<LinkedList<int>> A = InputMatrix(rowsA, colsA, "A");
        LinkedList<LinkedList<int>> B = InputMatrix(rowsB, colsB, "B");

        Console.WriteLine("\nМатрица A:");
        PrintMatrix(A);

        Console.WriteLine("\nМатрица B:");
        PrintMatrix(B);

        LinkedList<LinkedList<int>> C = MultiplyMatrices(A, B);

        Console.WriteLine("\nРезультат A × B:");
        PrintMatrix(C);
    }
}