using System;
using System.Threading;

namespace Lab21
{
    class Program
    {
        static byte[,] array = new byte[15, 15];
        static object lck = new object();

        static void Main(string[] args)
        {

            Console.WriteLine("");
            Random rnd = new Random();

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    int maxVal = (15-j)*16; 
                    array[i, j] = (byte)rnd.Next(4, 4+maxVal);  // время на обработку 1 участка, чем левее, тем дольше...
                }
            }

            Print();

            Console.ReadKey();

            ThreadStart threadStart1 = new ThreadStart(gardener1);
            Thread thread1 = new Thread(threadStart1);
            
            ThreadStart threadStart2 = new ThreadStart(gardener2);
            Thread thread2 = new Thread(threadStart2);    

            thread1.Start();
            thread2.Start();   
            
            Console.ReadKey();
        }

        static void gardener1()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    byte del = 0;
                    if (array[i, j] !=2)
                    {
                        del = array[i, j];
                        array[i, j] = 1;
                        Print();
                        Thread.Sleep(del*3);// первый садовник начинающий, ему нужно в 3 раза больше времени
                    }
                                  
                }                
               
            }
            lock (lck)
            {
                Print();
            }
        }

        static void gardener2()
        {
            for (int j = 14; j >= 0; j--)
            {
                for (int i = 14; i >= 0; i--)
                {
                    byte del = 0;
                    if (array[i, j] != 1)
                    {
                        del = array[i, j];
                        array[i, j] = 2;
                        Print();
                        Thread.Sleep(del); 
                    }
                   
                }
            }
            lock (lck)
            {
                Print();
            }
        }

        static void Print()
        {
            Console.Clear();
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < 15; j++)
                {
                    Console.Write("{0,3} ", array[i, j]);
                }

            }
        }
    }
}
