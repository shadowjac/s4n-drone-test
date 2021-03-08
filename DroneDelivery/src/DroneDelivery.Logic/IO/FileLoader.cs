using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DroneDelivery.Logic.IO
{
    public class FileLoader : ILoader<IEnumerable<IEnumerable<string>>>
    {
        public FileLoader(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }

        public string WorkingDirectory { get; }

        public IEnumerable<IEnumerable<string>> LoadInfo()
        {
            try
            {
                var files = Directory.GetFiles(WorkingDirectory, "in*.txt");
                var listOfOrders = new List<string[]>(files.Length);
                foreach (var file in files)
                {
                    var content = File.ReadAllLines(file);
                    listOfOrders.Add(content);
                }
                return listOfOrders;
            }
            catch (Exception ex) when (ex is IOException || ex is Exception)
            {
                return Enumerable.Empty<IEnumerable<string>>();
            }
        }
    }
}