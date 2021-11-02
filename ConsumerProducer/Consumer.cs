using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerProducer
{
    class Consumer
    {
        public event EventHandler OnTrigger;
        public List<int> BufferArray { get; set; }
        public int MaxCount { get; set; }
        private bool trigger;
        public bool Trigger
        {
            get { return trigger; }
            set
            {
                if (trigger != value)
                {
                    trigger = value;
                    FireOnTrigger();
                }
            }
        }

        private Random rand = new Random();
        int randNum;
        public Consumer(List<int> bufferArray, int maxCount)
        {
            this.BufferArray = bufferArray;
            this.MaxCount = maxCount;
        }

        public void Consume()
        {
            while (true)
            {
                Monitor.Enter(this);
                try
                {
                    if (BufferArray.Count == MaxCount)
                    {
                        randNum = rand.Next(0, BufferArray.Count);
                        BufferArray.RemoveAt(randNum);
                    }
                }
                finally
                {
                    Monitor.Exit(this);
                }
                Trigger = !Trigger;
                Thread.Sleep(1000);
            }
        }

        private void FireOnTrigger()
        {
            EventHandler handler = OnTrigger;
            if (handler != null)
            {
                if (Trigger)
                    OnTrigger($"Consumer har consumeret {randNum}.", EventArgs.Empty);
                else
                    OnTrigger($"Consumer waits...", EventArgs.Empty);
            }
        }
    }
}
