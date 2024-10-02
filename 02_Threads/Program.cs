namespace _02_Threads
{
    internal class Program
    {
        static void Method()
        {
            Console.WriteLine("Thread1 started!");
            for (int i = 0; i < 51; i++)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Thread1 end!");
        }
        static void ThreadFunk(object args)
        {
            Console.WriteLine("Thread2 started!");
            Array argArray = new object[2];
            argArray = (Array)args;
            int start = (int)argArray.GetValue(0);
            int end = (int)argArray.GetValue(1);
            for (int i = start; i < end; i++)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Thread2 end!");
        }
        static void ThreadFunk2(object args)
        {
            
            Array argArray = new object[3];
            argArray = (Array)args;
            int start = (int)argArray.GetValue(0);
            int end = (int)argArray.GetValue(1);
            int it = (int)argArray.GetValue(2);
            Console.WriteLine($"Thread({it}) started!");
            for (int i = start; i < end; i++)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine($"Thread({it})  end!");
        }

        static void Maximum(object args)
        {
            Console.WriteLine("Thread Max started!");
            int[] numbers = (int[]) args;
            Console.WriteLine("Maximum :"+numbers.Max());
            Console.WriteLine("Thread Max end!");
        }
        static void Minimum(object args)
        {
            Console.WriteLine("Thread Min started!");
            int[] numbers = (int[])args;
            Console.WriteLine("Minimum :" + numbers.Min());
            Console.WriteLine("Thread Min end!");
        }
        static void Serednie(object args)
        {
            Console.WriteLine("Thread Serednie started!");
            int[] numbers = (int[])args;
            long Suma = numbers.Sum();
            Console.WriteLine("Serednie :" + Suma /numbers.Length);
            Console.WriteLine("Thread Serednie end!");
        }
        static void SaveToFile(object args)
        {
            int[] numbers = (int[])args;
            string filePath = "Result.txt";

            // Запис чисел у файл
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    writer.WriteLine(numbers[i]);
                }
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }
            Console.WriteLine("Numbers have been written to the file.");
        }
        static void Main(string[] args)
        {
            //Thread thread = new Thread(Method);
            //thread.Start();
            //thread.Join();
            //object arg = new object[2] {1,5};
            //Thread thread1 = new Thread(ThreadFunk);
            //thread1.Start(arg);
            //thread1.Join();
            //Console.Write("Enter number of threads:");
            //int num = int.Parse(Console.ReadLine());
            //for (int i = 0; i < num; i++)
            //{
            //    object arg2 = new object[3] {1,10,i };
            //    Thread thread2 = new Thread(ThreadFunk2);
            //    thread2.Start(arg2);
            //}
            Random random = new Random();
            int[] numbers = new int[10000];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(-9999,9999); 
            }
            Thread thread3 = new Thread(Maximum);
            thread3.Start(numbers);
            Thread thread4 = new Thread(Minimum);
            thread4.Start(numbers);
            Thread thread5 = new Thread(Serednie);
            thread5.Start(numbers);
            Thread thread6 = new Thread(SaveToFile);
            thread6.Start(numbers);
        }
    }
}
