using DroneDelivery.Logic.Factory;
using DroneDelivery.Logic.IO;
using DroneDelivery.Logic.Translator;
using System;

namespace DroneDelivery.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var translator = new CoordinateTranslator();
            var drones = DroneCrewBuilder.Init()
                                .WithTranslator(translator)
                                .WithOrderLoader(new FileLoader(@"D:\s4n"))
                                .WithNavigateNotification((s, msg) => Console.WriteLine($"{s.Key} {msg}"))
                                .WithStartNavigationNotification((s, msg) => Console.WriteLine($"Drone {s.Key} starting delivery to {msg}"))
                                .Build();

            foreach (var drone in drones)
            {
                drone.Navigate();
            }
        }
    }
}