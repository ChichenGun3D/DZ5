using System;
using System.Collections.Generic;

class Grandma
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Diseases { get; set; } = new List<string>();
    public List<string> Medicines { get; set; } = new List<string>();

    public override string ToString()
    {
        string d = Diseases.Count == 0 ? "нет болезней" : string.Join(", ", Diseases);
        return $"{Name} ({Age} лет) — болезни: {d}";
    }
}

class Hospital
{
    public string Name { get; set; }
    public List<string> TreatableDiseases { get; set; } = new List<string>();
    public int Capacity { get; set; }
    public Stack<Grandma> Patients { get; set; } = new Stack<Grandma>();

    public override string ToString()
    {
        double percent = Capacity == 0 ? 0 : (double)Patients.Count / Capacity * 100;
        return $"{Name} — лечит: {string.Join(", ", TreatableDiseases)}; " +
               $"занято: {Patients.Count}/{Capacity} ({percent:F1}%)";
    }
}

class Program
{
    static void Main()
    {
        // ==== БОЛЬНИЦЫ ====
        List<Hospital> hospitals = new List<Hospital>
        {
            new Hospital
            {
                Name = "Городская больница №1",
                TreatableDiseases = new List<string> { "Грипп", "Простуда", "Ангина", "Давление" },
                Capacity = 3
            },
            new Hospital
            {
                Name = "Кардиоцентр",
                TreatableDiseases = new List<string> { "Давление", "Аритмия", "Инфаркт" },
                Capacity = 2
            },
            new Hospital
            {
                Name = "Инфекционная больница",
                TreatableDiseases = new List<string> { "Грипп", "Ковид", "Ангина" },
                Capacity = 4
            },
            new Hospital
            {
                Name = "Поликлиника №5",
                TreatableDiseases = new List<string> { "Простуда", "Гастрит" },
                Capacity = 2
            }
        };

        // ==== ОЧЕРЕДЬ БАБУЛЬ ====
        Queue<Grandma> grandmas = new Queue<Grandma>();

        Console.Write("Сколько бабуль пришло? ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"\n--- Бабуля #{i + 1} ---");
            Grandma g = new Grandma();

            Console.Write("Имя: ");
            g.Name = Console.ReadLine();

            Console.Write("Возраст: ");
            g.Age = int.Parse(Console.ReadLine());

            Console.Write("Сколько болезней? ");
            int dc = int.Parse(Console.ReadLine());

            for (int d = 0; d < dc; d++)
            {
                Console.Write($"Болезнь #{d + 1}: ");
                string disease = Console.ReadLine();
                g.Diseases.Add(disease);

                Console.Write($"Лекарство от {disease}: ");
                g.Medicines.Add(Console.ReadLine());
            }

            grandmas.Enqueue(g);
        }

        // ==== РАСПРЕДЕЛЕНИЕ ====
        List<Grandma> onStreet = new List<Grandma>();

        while (grandmas.Count > 0)
        {
            Grandma g = grandmas.Dequeue();

            // Бабуля без болезней — в первую свободную
            if (g.Diseases.Count == 0)
            {
                Hospital free = null;
                foreach (Hospital h in hospitals)
                {
                    if (h.Patients.Count < h.Capacity)
                    {
                        free = h;
                        break;
                    }
                }
                if (free != null)
                {
                    free.Patients.Push(g);
                    Console.WriteLine($"{g.Name} (без болезней) → {free.Name} (просто спросить)");
                }
                else
                {
                    onStreet.Add(g);
                    Console.WriteLine($"{g.Name} → все больницы заняты, плачет на улице");
                }
                continue;
            }

            // Ищем больницу, которая лечит > 50% болезней и не заполнена
            Hospital best = null;
            int bestMatched = -1;

            foreach (Hospital h in hospitals)
            {
                if (h.Patients.Count >= h.Capacity)
                    continue;

                int matched = 0;
                foreach (string disease in g.Diseases)
                {
                    if (h.TreatableDiseases.Contains(disease))
                        matched++;
                }

                double ratio = (double)matched / g.Diseases.Count;
                if (ratio > 0.5 && matched > bestMatched)
                {
                    best = h;
                    bestMatched = matched;
                }
            }

            if (best != null)
            {
                best.Patients.Push(g);
                Console.WriteLine($"{g.Name} → {best.Name} (лечит {bestMatched} из {g.Diseases.Count} болезней)");
            }
            else
            {
                onStreet.Add(g);
                Console.WriteLine($"{g.Name} → ни одна больница не берёт, плачет на улице");
            }
        }

        // ==== ВЫВОД ====
        Console.WriteLine("\n=== ВСЕ БАБУЛИ ===");
        foreach (Hospital h in hospitals)
        {
            foreach (Grandma g in h.Patients)
            {
                Console.WriteLine(g);
            }
        }
        foreach (Grandma g in onStreet)
        {
            Console.WriteLine(g + " [на улице]");
        }

        Console.WriteLine("\n=== БОЛЬНИЦЫ ===");
        foreach (Hospital h in hospitals)
        {
            Console.WriteLine(h);
            Console.WriteLine("  Пациенты:");
            if (h.Patients.Count == 0)
            {
                Console.WriteLine("   (пусто)");
            }
            else
            {
                foreach (Grandma g in h.Patients)
                {
                    Console.WriteLine("   - " + g.Name);
                }
            }
        }

        Console.WriteLine("\n=== НА УЛИЦЕ ===");
        if (onStreet.Count == 0)
        {
            Console.WriteLine("(никого)");
        }
        else
        {
            foreach (Grandma g in onStreet)
            {
                Console.WriteLine(g);
            }
        }
    }
}