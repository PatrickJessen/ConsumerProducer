using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsumerProducer
{
    class Program
    {
        // VIRKER IKKE SOM DET SKAL ENDNU
        static List<int> bufferArray;
        static int maxCount = 15;
        static void Main(string[] args)
        {
            bufferArray = new List<int>();
            InitBufferArray();

            Producer pro = new Producer(bufferArray, maxCount);
            Consumer cons = new Consumer(bufferArray, maxCount);

            pro.OnTrigger += Pro_OnTrigger;
            cons.OnTrigger += Cons_OnTrigger;

            Thread t1 = new Thread(pro.Produce);
            Thread t2 = new Thread(cons.Consume);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.Read();
        }

        private static void Cons_OnTrigger(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        private static void Pro_OnTrigger(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        static void InitBufferArray()
        {
            for (int i = 0; i < 10; i++)
                bufferArray.Add(i);
        }
    }
}
