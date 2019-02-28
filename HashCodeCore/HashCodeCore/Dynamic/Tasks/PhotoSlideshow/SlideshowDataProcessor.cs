using System.Collections.Generic;
using System.IO;

namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class SlideshowDataProcessor : IDataProcessor
    {
        public SlideshowData Data { get; private set; }
        
        public void Process(string inputPath)
        {
            Data = new SlideshowData(File.ReadAllLines(inputPath));
        }
        
        public void Store(string outputPath)
        {
            File.WriteAllLines(outputPath, Data.Stringify());
        }

        public void Store(SlideshowData data, string outputPath)
        {
            Data = data;
            Store(outputPath);
        }
    }
}