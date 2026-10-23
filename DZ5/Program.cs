using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void CountLetters(List<char> text, out int vowels, out int consonants)
    {
        vowels = 0;
        consonants = 0;

        string vowelLetters = "аеёиоуыэюяАЕЁИОУЫЭЮЯaeiouyAEIOUY";
        string consonantLetters = "бвгджзйклмнпрстфхцчшщъьБВГДЖЗЙКЛМНПРСТФХЦЧШЩЪЬbcdfghjklmnpqrstvwxzBCDFGHJKLMNPQRSTVWXZ";

        for (int i = 0; i < text.Count; i++)
        {
            if (vowelLetters.IndexOf(text[i]) != -1)
                vowels++;
            else if (consonantLetters.IndexOf(text[i]) != -1)
                consonants++;
        }
    }

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Укажите имя файла!");
            return;
        }

        string text = File.ReadAllText(args[0]);
        List<char> chars = new List<char>(text.ToCharArray());

        int vowels, consonants;
        CountLetters(chars, out vowels, out consonants);

        Console.WriteLine("Гласных: " + vowels);
        Console.WriteLine("Согласных: " + consonants);
    }
}