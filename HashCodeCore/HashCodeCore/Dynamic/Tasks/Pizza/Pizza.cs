using System;

namespace HashCodeCore.Dynamic.Tasks.Pizza
{
    public static class Pizza
    {
        public static void Run()
        {
            const string inputFolder = "./data/pizza/";
            const string outputFolder = "./output/pizza/";
            string[] files =
            {
                "a_example.in", "b_small.in", "c_medium.in", "d_big.in"
            };

            for (var i = 0; i < files.Length; i++)
            {
                Console.WriteLine("Solving {0}", inputFolder + files[i]);
                var problem = new PizzaProblem(inputFolder + files[i], new PizzaDataProcessor());
                problem.Solve();
                problem.Store(outputFolder + files[i]);
            }
        }
    }
}