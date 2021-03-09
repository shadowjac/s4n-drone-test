using DroneDelivery.Logic;
using System.Linq;

internal static class Program
{
    public static void Main(string[] args)
    {
        if (args.Any() && args[0] == "1")
            DeliveryHandler.Start();
        else DeliveryHandler.StartAsync();
    }
}