using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCharacteristicsModeling
{
    class Program
    {
        private static Random rand = new Random();

        private static int[,] regularOper = {{56,    0}, //{operations count, block number}
                                             {90,   1},
                                             {100,   3},
                                             {26,    6},
                                             {69,    7}};

        private static int[,] fileOper = {{100, 2, 2}, //{count of operations with file, file number, block number}
                                          {256, 3, 4},
                                          {84, 2, 5}};

        private static double[] n = new double[8];
        private static double[] V = new double[8];

        private static int cycleIterations = 10000;

        private static void algo()
        {
            int i, j, N;

            //V1
            V[0]++;

            //V2
            V[1]++;

            Check_i:
            {
                i = random(-3, 3);
                if (i < 0)
                {
                    //V2
                    V[1]++;

                    goto Check_i;
                }
            }

            //V3
            V[2]++;

            j = random(0, 5);
            if (j < 3)
            {
                //V4
                V[3]++;

                goto Check_i;
            }

            //V5
            V[4]++;

            //V6
            V[5]++;

            N = random(4, 10);
            if (N == 4)
            {
                //V7
                V[6]++;

                goto Check_i;
            }

            //V8
            V[7]++;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < cycleIterations; i++)
            {
                algo();
            }

            //n calculation
            Console.WriteLine("n calculation:");
            for (int i = 0; i < n.Length; i++)
            {
                n[i] = V[i] / cycleIterations;
                Console.WriteLine("n" + (i + 1) + " = " + n[i]);
            }

            Console.WriteLine("\n");

            double N = 0; // average operations count with one run of the algorithm
            double operNum = 0;
            for (int i = 0; i < regularOper.GetLength(0); i++)
            {
                operNum += (n[regularOper[i,1]] * regularOper[i,0]);
                N += n[regularOper[i,1]];
            }
            Console.WriteLine("Average operations count with one run of the algorithm: " + operNum + "\n");

            double appealNum;   // average count of requests to file
            double infoNum;     // average amount of informations which transmitted durring one request to file
            double fileNum;     // file number
            for (int i = 0; i < 3; i++)
            {
                appealNum = 0;
                infoNum = 0;
                fileNum = i + 1;

                //average count of requests to file
                for (int j = 0; j < fileOper.GetLength(0); j++)
                {
                    if (fileNum == fileOper[j,1])
                        appealNum += n[fileOper[j,2]];
                }
                Console.WriteLine("Average count of requests to file " + (i + 1) + ": " + appealNum);

                //average amount of informations which transmitted durring one request to file
                for (int j = 0; j < fileOper.GetLength(0); j++)
                {
                    if (fileNum == fileOper[j,1])
                        infoNum += (n[fileOper[j,2]] * fileOper[j,0]);
                }
                Console.WriteLine("Average amount of informations which transmitted durring one request to file " + (i + 1) + ": " + (infoNum / appealNum) + "\n");
            }
            Console.WriteLine("Average laboriousness of one calculation stage:" + (operNum / N));

            Console.ReadKey();
        }

        private static int random(int min, int max)
        {
            return rand.Next((max - min) + 1) + min;
        }
    }
}
