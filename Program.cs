namespace _13._12Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Text.Json;


//class Program
//{
//    static void Main()
//    {
//        Random rand = new Random();
//        List<int> numbers = Enumerable.Range(0, 100).Select(i => rand.Next(1, 100)).ToList();
//        List<int> primes = numbers.Where(Prime).ToList();
//        List<int> fib = numbers.Where(Fib).ToList();

//        Console.WriteLine("Сгенерированные числа:");
//        Console.WriteLine(string.Join(", ", numbers));

//        Console.WriteLine("Числа Фибоначчи");
//        Console.WriteLine(string.Join(",", fib));
//        Console.WriteLine("Числа Простые");
//        Console.WriteLine(string.Join(",", primes));

//        File.WriteAllLines("primes.txt",primes.Select(n => n.ToString()));
//        File.WriteAllLines("fibonacci.txt", fib.Select(n => n.ToString()));

//        Console.WriteLine("\nФайлы созданы.");
//    }
//    static bool Prime(int n)
//    {
//        if(n < 2) return false;
//        for(int i  = 2; i <= Math.Sqrt(n); i++)
//        {
//            if(n % i == 0) return false;
//        }
//        return true;
//    }
//    static bool Fib(int n)
//    {
//        int a = 0, b = 1;
//        while(b < n)
//        {
//            int temp = a;
//            a = b;
//            b = temp + b;
//        }
//        return n == b || n == 0;
//    }
//}
//class Program 
//{
//    static void Main()
//    {
//        string filePath = "sample.txt";

//        File.WriteAllText(filePath, "Пример текста для поиска слова и замены.");
//        Console.WriteLine("Исходное содержимое файла:");
//        Console.WriteLine(File.ReadAllText(filePath));

//        Console.Write("Введите слово для поиска: ");
//        string searchWord = Console.ReadLine();

//        Console.Write("Введите слово для замены: ");
//        string replaceWord = Console.ReadLine();

//        string content = File.ReadAllText(filePath);

//        string updatedContent = content.Replace(searchWord, replaceWord);

//        File.WriteAllText(filePath, updatedContent);


//        Console.WriteLine("\nОбновленное содержимое файла:");
//        Console.WriteLine(File.ReadAllText(filePath));
//    }
//}

//class Program
//{
//    static void Main(string[] args)
//    {
//        try
//        {

//            string filename = "sample.txt";

//            Console.WriteLine("Введите слово для поиска: ");

//            string search = Console.ReadLine();
//            Console.WriteLine("Введите слово для замены: ");

//            string change = Console.ReadLine();
//            string filecontent = File.ReadAllText(filename);
//            int count = Podshet(filecontent, search);
//            string newword = filecontent.Replace(search, change);
//            File.WriteAllText(filename, newword);
//            Console.WriteLine($"Количество замененных слов: {count}");

//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.Message);
//        }

//        static int Podshet(string text, string word)
//        {
//            int count = 0;
//            int index = text.IndexOf(word, StringComparison.OrdinalIgnoreCase);
//            while (index != -1)
//            {
//                count++;
//                index = text.IndexOf(word, index + word.Length, StringComparison.OrdinalIgnoreCase);
//            }
//            return count;
//        }
//    }
//}

//class Program
//{
//    [Serializable]
//    public class Text
//    {
//        public string Content { get; set; }
//        public Text(string content)
//        {
//            Content = content;
//        }
//    }

//    static void Main(string[] args)
//    {

//        string filePath = "sample.txt";

//        if (!File.Exists(filePath))
//        {
//            Console.WriteLine("Файл не найден.");
//            return;
//        }

//        string newFilePath = ReverseContent(filePath);
//        Console.WriteLine($"Новый файл создан: {newFilePath}");
//    }

//    static void SaveContent(string filePath, Text textfile)
//    {
//        File.WriteAllText(filePath, textfile.Content);
//    }

//    static void ReverseText(Text textfile)
//    {
//        char[] reversedArray = textfile.Content.ToCharArray();
//        Array.Reverse(reversedArray);
//        textfile.Content = new string(reversedArray);
//    }

//    static Text ReadFileContent(string filePath)
//    {
//        string text = File.ReadAllText(filePath);
//        return new Text(text);
//    }

//    static string ReverseContent(string filePath)
//    {
//        Text textfile = ReadFileContent(filePath);
//        ReverseText(textfile);
//        string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), "перевернут_" + Path.GetFileName(filePath));
//        SaveContent(newFilePath, textfile);
//        return newFilePath;
//    }
//}


class Program
{
    [Serializable]
    public class Text
    {
        public string Content {  get; set; }
        public Text(string content) 
        {
            Content = content;
        }
    }
    static void Main(string[] args)
    {
        string filePath = "numbers.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        AnalyzeFile(filePath);
    }
    static void AnalyzeFile(string filePath)
    {
        Text textfile = ReadFileContent(filePath);
        List<int> numbers = ParseNumbers(textfile);
        Dictionary<string, List<int>> analysis = AnalyzeNumbers(numbers);
        SaveAnalysis(analysis);
        Console.WriteLine($"Положительных чисел: {analysis["positives"].Count}");
        Console.WriteLine($"Отрицательных чисел: {analysis["negatives"].Count}");
        Console.WriteLine($"Двузначных чисел: {analysis["twoDigits"].Count}");
        Console.WriteLine($"Пятизначных чисел: {analysis["fiveDigits"].Count}");
    }
    static Text ReadFileContent(string filePath)
    {
        string text = File.ReadAllText(filePath);
        return new Text(text);
    }
    static void SaveAnalysis(Dictionary<string, List<int>> analysis)
    {
        foreach(var item in analysis)
        {
            File.WriteAllLines($"{item.Key}.txt", item.Value.ConvertAll(n => n.ToString()));
        }
    }
    static List<int> ParseNumbers(Text textfile)
    {
        string[] numberStrings = textfile.Content.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        List<int> numbers = new List<int>();
        foreach (string numberString in numberStrings)
        {
            if (int.TryParse(numberString, out int number))
            {
                numbers.Add(number);
            }
        }
        return numbers;
    }
    static Dictionary<string, List<int>> AnalyzeNumbers(List<int> numbers)
    {
        Dictionary<string, List<int>> analysis = new Dictionary<string, List<int>>
        {
            { "positives", new List<int>() },
            { "negatives", new List<int>() },
            { "twoDigits", new List<int>() },
            { "fiveDigits", new List<int>() }
        };

        foreach (int number in numbers)
        {
            if (number > 0) analysis["positives"].Add(number);
            if (number < 0) analysis["negatives"].Add(number);
            if (Math.Abs(number) >= 10 && Math.Abs(number) < 100) analysis["twoDigits"].Add(number);
            if (Math.Abs(number) >= 10000 && Math.Abs(number) < 100000) analysis["fiveDigits"].Add(number);
        }
        return analysis;
    }
}