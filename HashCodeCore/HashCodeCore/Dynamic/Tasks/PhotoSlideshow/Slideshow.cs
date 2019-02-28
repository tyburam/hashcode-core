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
                Console.WriteLine("Solving {0}", inputFolder + t);
                var problem = new SlideshowProblem(inputFolder + t, new SlideshowDataProcessor());
                problem.Solve();
                problem.Store(outputFolder + t);
            }
        }
    }
}