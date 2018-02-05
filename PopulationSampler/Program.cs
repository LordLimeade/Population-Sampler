using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopulationSampler.Classes;

namespace PopulationSampler
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean check = true;
            List<string> data = new List<string>();
            List<int> dataInt = new List<int>();

            for (int x = 0; x < 4; x++)
            {
                data.Add("");
            }

            while (check)
            {
                Console.Clear();
                check = gatherAndVerifyData(data);
            }

            foreach(string s in data)
            {
                dataInt.Add(int.Parse(s));
            }


            runCalculations(dataInt);
            

            Console.ReadKey();
        }
        private static Boolean gatherAndVerifyData(List<string> data)
        {
            string yesNo = "";
            Boolean check = false;

            input(data);

            yesNo = verifyData(data);

            while (yesNo.ToLower() != "y" && yesNo.ToLower() != "n")
            {
                Console.WriteLine("Enter Y or N to continue...");
                yesNo = Console.ReadLine();
            }

            if (yesNo.ToLower() == "n")
            {
                check = true;
            }

            return check;
        }

        private static void input(List<string> sampleData)
        {
            Console.WriteLine("Enter your population size.");
            sampleData[0] = Console.ReadLine();
            Boolean check = true;



            //Get the population size
            while(validateInt(sampleData[0]))
            {
                Console.Clear();
                Console.WriteLine("Population size must be an integer. Please try again...");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("Enter your population size.");
                sampleData[0] = Console.ReadLine();
            }



            //Get the sample size lower limit
            //Later used for random sample size within a range
            Console.WriteLine("Enter your sample's lower limit.");
            sampleData[1] = Console.ReadLine();

            while (validateInt(sampleData[1]))
            {
                Console.Clear();
                Console.WriteLine("Your sample's lower limit must be an integer. Please try again...");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("Enter your sample's lower limit.");
                sampleData[1] = Console.ReadLine();
            }



            //Get the sample size upper limit
            //Later used for random sample size within a range
            Console.WriteLine("Enter your sample's upper limit.");
            sampleData[2] = Console.ReadLine();

            if (!validateInt(sampleData[2]))
            {
                check = false;
                if (int.Parse(sampleData[2]) < int.Parse(sampleData[1]))
                {
                    check = true;
                }
            }

            while (check)
            {
                if (validateInt(sampleData[2]))
                {
                    Console.Clear();
                    Console.WriteLine("Your sample's upper limit must be an integer. Please try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Your sample's upper limit must be greater than your sample's lower limit. Please try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
                

                Console.WriteLine("Enter your sample's upper limit.");
                sampleData[2] = Console.ReadLine();

                if (!validateInt(sampleData[2]))
                {
                    check = false;
                    if (int.Parse(sampleData[2]) < int.Parse(sampleData[1]))
                    {
                        check = true;
                    }
                }
            }



            //Get the number of samples to take
            Console.WriteLine("Enter the number of samples to take.");
            sampleData[3] = Console.ReadLine();

            while (validateInt(sampleData[3]))
            {
                Console.Clear();
                Console.WriteLine("Number of samples must be an integer. Please try again...");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("Enter the number of samples to take.");
                sampleData[3] = Console.ReadLine();
            }
        }

        private static Boolean validateInt(string x)
        {
            Boolean check = false;
            int hold = 0;

            try
            {
                hold = int.Parse(x);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                check = true;
            }

            return check;
        }

        private static string verifyData(List<string> data)
        {
            string yesNo = "n";

            Console.WriteLine("Population Size: " + data[0]);
            Console.WriteLine("Sample Lower Limit: " + data[1]);
            Console.WriteLine("Sample Upper Limit: " + data[2]);
            Console.WriteLine("Number of Samples to Take: " + data[3]);

            Console.WriteLine();
            Console.WriteLine("Is this data correct? (Y/N)");

            yesNo = Console.ReadLine();

            return yesNo;
        }

        private static void runCalculations(List<int> data)
        {
            Console.Clear();

            Random rnd = new Random();
            Sample population = new Sample(data[3], data[1], data[2]);
            List<int> sampleData = new List<int>();
            int[,] SchnabelData = new int[population.SamplesTaken, 4];


            for(int x = 0; x < 4; x++)
            {
                sampleData.Add(0);
            }


            for (int y = 1; y <= data[0]; y++)
            {
                population.Creatures.Add(new Creature(y));
            }


            for (int x = 0; x < population.SamplesTaken; x++)
            {
                selectSample(population, rnd);
                countMarks(population,sampleData);

                for (int y = 0; y < 4; y++)
                {
                    SchnabelData[x, y] = sampleData[y];
                }

                displayData(sampleData, x + 1, population.SamplesTaken, SchnabelData);
            }
        }

        private static void selectSample(Sample pop, Random rnd)
        {
            int caught = rnd.Next(pop.LowerLimit, pop.UpperLimit + 1);

            foreach(Creature c in pop.Creatures)
            {
                c.Selected = false;
            }

            while (pop.countSelected() < caught)
            {
                int id = rnd.Next(1, pop.Creatures.Count + 1);

                Creature creature = pop.Creatures.Where(i => id == i.ID).First();

                if (!creature.Selected)
                {
                    creature.Selected = true;
                }
            }
        }

        private static void countMarks(Sample pop, List<int> data)
        {
            List<Creature> catches = new List<Creature>();
            int marked = 0, notMarked = 0;

            data[0] = pop.countSelected();

            foreach(Creature c in pop.Creatures)
            {
                if (c.Selected)
                {
                    catches.Add(c);
                }
            }

            foreach(Creature m in catches)
            {
                if (m.Marked)
                {
                    marked++;
                }
                else
                {
                    notMarked++;
                }
            }

            data[1] = marked;
            data[2] = notMarked;
            data[3] = pop.countMarks();

            foreach(Creature x in catches)
            {
                Creature hold = pop.Creatures.Where(i => x.ID == i.ID).First();
                hold.Marked = true;
            }
        }

        private static void displayData(List<int> data, int sampleCount, int maxCount, int[,] ShcnabelData)
        {
            Console.WriteLine();
            Console.WriteLine("Sample Size Taken: " + data[0].ToString());
            Console.WriteLine("Number of Previously Marked Creatures: " + data[1].ToString());
            Console.WriteLine("Number of Newly Marked Creatures: " + data[2].ToString());
            Console.WriteLine("Number of Previously Marked Creatures: " + data[3].ToString());

            if (sampleCount == 2)
            {
                int lp = (ShcnabelData[0,0] * ShcnabelData[1, 0]) / ShcnabelData[1, 1];
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(String.Format("Lincoln-Peterson Index: {0:###,###,###,##0}", lp));
            }

            if (sampleCount == maxCount)
            {
                double numerator = 0, denominator = 0;

                for(int x = 0; x < maxCount; x++)
                {
                    numerator += (ShcnabelData[x, 3] * ShcnabelData[x, 0]);
                    denominator += ShcnabelData[x, 1];

                }
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(String.Format("Schnabel Index: {0:###,###,###,##0}", numerator/denominator));
            }
        }
    }
}
