using System.IO;

namespace HashCodeCore.Dynamic.Tasks.Pizza
{
    public class PizzaDataProcessor : IDataProcessor
    {
        public PizzaData Data { get; private set; }
        
        public void Process(string inputPath)
        {
            var content = File.ReadAllLines(inputPath);
            
            var first = content[0].Split(' ');
            Data = new PizzaData(new int[4]
            {
                int.Parse(first[0]), int.Parse(first[1]), int.Parse(first[2]), int.Parse(first[3])
                
            });

            for (var i = 1; i < content.Length; i++)
            {
                for (var j = 0; j < content[i].Length; j++)
                {
                    Data.AddIngredient(i-1, j, content[i][j]);
                }
            }
        }

        public void Store(string outputPath)
        {
            File.WriteAllLines(outputPath, Data.Stringify());
        }

        public void Store(PizzaData data, string outputPath)
        {
            Data = data;
            Store(outputPath);
        }
    }
}