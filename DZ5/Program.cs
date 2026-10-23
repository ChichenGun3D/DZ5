using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<ImageItem> images = new List<ImageItem>();
        for (int i = 0; i < 32; i++)
        {
            string url = $"https://example.com/image_{i}.jpg";
            images.Add(new ImageItem { Url = url, OriginalIndex = i });
            images.Add(new ImageItem { Url = url, OriginalIndex = i });
        }
        Console.WriteLine("=== Исходный список ===");
        for (int i = 0; i < images.Count; i++)
        {
            Console.Write(images[i].OriginalIndex + " ");
        }
        Console.WriteLine("\n");
        Random rnd = new Random();
        for (int i = images.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            ImageItem temp = images[i];
            images[i] = images[j];
            images[j] = temp;
        }

        Console.WriteLine("=== Перемешанный список ===");
        for (int i = 0; i < images.Count; i++)
        {
            Console.Write(images[i].OriginalIndex + " ");
        }
        Console.WriteLine();
    }
}

class ImageItem
{
    public string Url { get; set; }
    public int OriginalIndex { get; set; }
}