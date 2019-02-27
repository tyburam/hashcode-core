namespace HashCodeCore.Dynamic
{
    public interface IDataProcessor
    {
        void Process(string inputPath);
        void Store(string outputPath);
    }
}