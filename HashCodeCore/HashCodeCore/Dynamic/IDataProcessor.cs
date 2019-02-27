namespace dp_net.Dynamic
{
    public interface IDataProcessor
    {
        void Process(string inputPath);
        void Store(string outputPath);
    }
}