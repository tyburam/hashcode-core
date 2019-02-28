namespace HashCodeCore.Dynamic.Tasks.PhotoSlideshow
{
    public class SlideshowProblem : Problem
    {
        private SlideshowDataProcessor _processor;
        private SlideshowData _data;
        
        public SlideshowProblem(string inputPath, IDataProcessor processor) : base(inputPath, processor)
        {
            _processor = (processor as SlideshowDataProcessor);
            _processor.Process(inputPath);
            _data = _processor.Data;
        }

        public override void Solve()
        {
            _data.Sort();
            
            var i = 0;
            while (i < _data.Photos.Count)
            {
                if (_data.Photos[i].IsHorizontal)
                {
                    _data.Slides.Add(new Slide(_data.Photos[i].Id));
                    ++i;
                    continue;
                }
                
                _data.Slides.Add(new Slide(_data.Photos[i].Id, _data.Photos[i+1].Id));
                i += 2;
            }
        }
        
        public override void Store(string outputPath)
        {
            _processor.Store(_data, outputPath);
        }
    }
}