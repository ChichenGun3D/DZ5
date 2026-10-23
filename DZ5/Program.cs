using System;
using System.Collections.Generic;
using System.IO;

class Student
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int BirthYear { get; set; }
    public string Exam { get; set; }
    public int Score { get; set; }

    public override string ToString()
    {
        return $"{LastName} {FirstName}, {BirthYear}, {Exam}, {Score}";
    }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>();

        // Чтение студентов из файла
        string fileName = "students.txt";
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (line.Trim() == "") continue;

                string[] parts = line.Split(';');
                Student s = new Student
                {
                    LastName = parts[0],
                    FirstName = parts[1],
                    BirthYear = int.Parse(parts[2]),
                    Exam = parts[3],
                    Score = int.Parse(parts[4])
                };
                students.Add(s);
            }
        }
        else
        {
            Console.WriteLine($"Файл {fileName} не найден, список пуст.");
        }

        // Меню
        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("Новый студент — добавить");
            Console.WriteLine("Удалить — удалить по фамилии и имени");
            Console.WriteLine("Сортировать — сортировка по баллам");
            Console.WriteLine("Вывести — показать всех");
            Console.WriteLine("Выход — завершить");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            if (choice == "Новый студент")
            {
                Student s = new Student();

                Console.Write("Фамилия: ");
                s.LastName = Console.ReadLine();

                Console.Write("Имя: ");
                s.FirstName = Console.ReadLine();

                Console.Write("Год рождения: ");
                s.BirthYear = int.Parse(Console.ReadLine());

                Console.Write("Экзамен: ");
                s.Exam = Console.ReadLine();

                Console.Write("Баллы: ");
                s.Score = int.Parse(Console.ReadLine());

                students.Add(s);
                Console.WriteLine("Студент добавлен.");
            }
            else if (choice == "Удалить")
            {
                Console.Write("Фамилия: ");
                string last = Console.ReadLine();

                Console.Write("Имя: ");
                string first = Console.ReadLine();

                Student found = null;
                foreach (Student s in students)
                {
                    if (s.LastName == last && s.FirstName == first)
                    {
                        found = s;
                        break;
                    }
                }

                if (found != null)
                {
                    students.Remove(found);
                    Console.WriteLine("Студент удалён.");
                }
                else
                {
                    Console.WriteLine("Студент не найден.");
                }
            }
            else if (choice == "Сортировать")
            {
                for (int i = 0; i < students.Count - 1; i++)
                {
                    for (int j = 0; j < students.Count - 1 - i; j++)
                    {
                        if (students[j].Score > students[j + 1].Score)
                        {
                            Student temp = students[j];
                            students[j] = students[j + 1];
                            students[j + 1] = temp;
                        }
                    }
                }
                Console.WriteLine("Отсортировано по баллам (по возрастанию).");
            }
            else if (choice == "Вывести")
            {
                if (students.Count == 0)
                {
                    Console.WriteLine("Список пуст.");
                }
                else
                {
                    for (int i = 0; i < students.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + students[i]);
                    }
                }
            }
            else if (choice == "Выход")
            {
                break;
            }
            else
            {
                Console.WriteLine("Неизвестная команда.");
            }
        }
    }
}