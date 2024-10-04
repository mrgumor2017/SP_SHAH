using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace _03_Lock
{
    class Stat
    {
        public int Letters { get; set; }
        public int Digits { get; set; }
        public int Words { get; set; }         
        public int PunctuationMarks { get; set; } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stat statistic = new Stat();
            string[] files = Directory.GetFiles($@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\Test");
            Thread[] threads = new Thread[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                string text = File.ReadAllText(files[i]);

                threads[i] = new Thread(TextAnalyse);
                threads[i].Start(new object[] { text, statistic });
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine($"Number of Letters: {statistic.Letters}");
            Console.WriteLine($"Number of Digits: {statistic.Digits}");
            Console.WriteLine($"Number of Words: {statistic.Words}");
            Console.WriteLine($"Number of Punctuation Marks: {statistic.PunctuationMarks}");
        }

        static void TextAnalyse(object param)
        {
            object[] parameters = (object[])param;
            string text = (string)parameters[0];
            Stat statistic = (Stat)parameters[1];

            int wordCount = 0;
            int punctuationCount = 0;
            int letterCount = 0;
            int digitCount = 0;
            string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            wordCount = words.Length;
            punctuationCount = text.Count(char.IsPunctuation);
            letterCount = text.Count(char.IsLetter);
            digitCount = text.Count(char.IsDigit);

            lock (statistic)
            {
                statistic.Letters += letterCount;
                statistic.Digits += digitCount;
                statistic.Words += wordCount;
                statistic.PunctuationMarks += punctuationCount;
            }
        }
    }
}
