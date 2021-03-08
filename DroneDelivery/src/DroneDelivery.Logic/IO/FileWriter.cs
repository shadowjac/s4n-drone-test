using System.IO;

namespace DroneDelivery.Logic.IO
{
    public class FileWriter : IWriter
    {
        public FileWriter(string outputPath)
        {
            OutputPath = outputPath;
        }

        public string OutputPath { get; }

        public void Write(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                File.WriteAllText(OutputPath, message);
        }
    }
}