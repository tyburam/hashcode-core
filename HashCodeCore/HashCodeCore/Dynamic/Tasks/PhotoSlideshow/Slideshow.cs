using System;

namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class Slideshow
    {
        public static void Run()
        {
            const string inputFolder = "./data/slideshow/";
            const string outputFolder = "./output/slideshow/";
            string[] files =
            {
                "a_example.txt", "b_lovely_landscapes.txt", "c_memorable_moments.txt", "d_pet_pictures.txt", 
                "e_shiny_selfies.txt"
            };

            foreach (var t in files)
            {
                Console.WriteLine("[{0}] Solving {1}", DateTime.Now, inputFolder + t);
                var problem = new SlideshowProblem(inputFolder + t, new SlideshowDataProcessor());
                Console.WriteLine("[{0}] Problem loaded", DateTime.Now);
                problem.Solve();
                Console.WriteLine("[{0}] Problem solved", DateTime.Now);
                problem.Store(outputFolder + t);
                Console.WriteLine("[{0}] Problem stored", DateTime.Now);
            }
        }
    }
}