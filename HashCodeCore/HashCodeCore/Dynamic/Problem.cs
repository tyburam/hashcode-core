namespace HashCodeCore.Dynamic
{
    public class Problem
    {
        private string inputPath;
        private IDataProcessor processor;
        
        public Problem(string inputPath, IDataProcessor processor)
        {
            this.inputPath = inputPath;
            this.processor = processor;
        }

        public virtual void Solve()
        {
        }

        public virtual void Store(string outputPath)
        {
            
        }
    }
}