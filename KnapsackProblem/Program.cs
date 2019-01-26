using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GAF;
using GAF.Operators;

namespace KnapsackProblem
{
    class Program
    {
        static List<Bag.Item> knapsackItems;

        static void Main(string[] args)
        {
            knapsackItems = new List<Bag.Item>();
            knapsackItems.Add(new Bag.Item() { Name = "Map", Weight = 9, Value = 150 });
            knapsackItems.Add(new Bag.Item() { Name = "Water", Weight = 153, Value = 200 });
            knapsackItems.Add(new Bag.Item() { Name = "Compass", Weight = 13, Value = 35 });
            knapsackItems.Add(new Bag.Item() { Name = "Sandwitch", Weight = 50, Value = 160 });
            knapsackItems.Add(new Bag.Item() { Name = "Glucose", Weight = 15, Value = 60 });
            knapsackItems.Add(new Bag.Item() { Name = "Tin", Weight = 68, Value = 45 });
            knapsackItems.Add(new Bag.Item() { Name = "Banana", Weight = 27, Value = 60 });
            knapsackItems.Add(new Bag.Item() { Name = "Apple", Weight = 39, Value = 40 });
            knapsackItems.Add(new Bag.Item() { Name = "Cheese", Weight = 23, Value = 30 });
            knapsackItems.Add(new Bag.Item() { Name = "Beer", Weight = 52, Value = 10 });
            knapsackItems.Add(new Bag.Item() { Name = "Suntan Cream", Weight = 11, Value = 70 });
            knapsackItems.Add(new Bag.Item() { Name = "Camera", Weight = 32, Value = 30 });
            knapsackItems.Add(new Bag.Item() { Name = "T-shirt", Weight = 24, Value = 15 });
            knapsackItems.Add(new Bag.Item() { Name = "Trousers", Weight = 48, Value = 10 });
            knapsackItems.Add(new Bag.Item() { Name = "Umbrella", Weight = 73, Value = 40 });
            knapsackItems.Add(new Bag.Item() { Name = "WaterProof Trousers", Weight = 42, Value = 70 });
            knapsackItems.Add(new Bag.Item() { Name = "Note-Case", Weight = 22, Value = 80 });
            knapsackItems.Add(new Bag.Item() { Name = "Sunglasses", Weight = 7, Value = 20 });
            knapsackItems.Add(new Bag.Item() { Name = "Towel", Weight = 18, Value = 12 });
            knapsackItems.Add(new Bag.Item() { Name = "Socks", Weight = 4, Value = 50 });
            knapsackItems.Add(new Bag.Item() { Name = "Book", Weight = 30, Value = 10 });
            knapsackItems.Add(new Bag.Item() { Name = "waterproof overclothes ", Weight = 43, Value = 75 });

            const double crossoverProbability = 0.65;
            const double mutationProbability = 0.08;
            const int elitismPercentage = 5;

            //create the population
            var population = new Population(100, 22, false, false);

            //create the genetic operators 
            var elite = new Elite(elitismPercentage);

            var crossover = new Crossover(crossoverProbability, true)
            {
                CrossoverType = CrossoverType.SinglePoint
            };

            var mutation = new BinaryMutate(mutationProbability, true);

            //create the GA itself 
            var ga = new GeneticAlgorithm(population, EvaluateFitness);

            //subscribe to the GAs Generation Complete event 
            ga.OnGenerationComplete += ga_OnGenerationComplete;

            //add the operators to the ga process pipeline 
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutation);

            //run the GA 
            ga.Run(TerminateAlgorithm);

            
            //get the best solution and print Items
            var Bestchromosome = ga.Population.GetTop(1)[0];

            //decode chromosome
            Console.WriteLine("Maximum value of knapsack contains these Items : "); 
            Bag bag = new Bag();
            for (int i = 0; i < Bestchromosome.Count; i++)
            {
                if (Bestchromosome.Genes[i].BinaryValue == 1)
                {
                    Console.WriteLine(knapsackItems[i].ToString()); 
                }
            }

            Console.ReadKey(); 
        }

        public static double EvaluateFitness(Chromosome chromosome)
        {
            double fitnessValue = 0;
          
            //decode chromosome
            Bag bag = new Bag();
            for (int i = 0; i < chromosome.Count; i++)
            {
                if (chromosome.Genes[i].BinaryValue == 1)
                {
                    bag.AddItem(knapsackItems[i]);
                }
            }

            

            if (bag.TotalWeight <= 400)
            {
                //put fitnessValue between 0 and 1
                fitnessValue = (bag.TotalValue / Convert.ToDouble(10000));
                //Console.WriteLine(" ----------------------- Total Weight : {0}   Total Value : {1} Fitness : {2}  ", bag.TotalWeight, bag.TotalWeight, fitnessValue.ToString());
            }

            return fitnessValue;
        }

        public static bool TerminateAlgorithm(Population population, int currentGeneration, long currentEvaluation)
        {
            return currentGeneration > 50;
        }

        private static void ga_OnGenerationComplete(object sender, GaEventArgs e)
        {
            //get the best solution on each generation
            var chromosome = e.Population.GetTop(1)[0];

            //decode chromosome
            Bag bag = new Bag();
            for (int i = 0; i < chromosome.Count; i++)
            {
                if (chromosome.Genes[i].BinaryValue == 1)
                {
                    bag.AddItem(knapsackItems[i]); 
                }
            }

            //display fitness of the best chromosome in this generation 
            Console.WriteLine(" GenerationComplete. Best choice is: Total Weight : {0}   Total Value : {1} Fitness : {2}", bag.TotalWeight, bag.TotalValue , e.Population.MaximumFitness);

        }
    }
}
