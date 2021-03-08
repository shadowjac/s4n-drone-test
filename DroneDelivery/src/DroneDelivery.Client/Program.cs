using DroneDelivery.Logic;
using DroneDelivery.Logic.Factory;
using DroneDelivery.Logic.Translator;
using System;
using System.Collections.Generic;

namespace DroneDelivery.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var plan1 = new string[] { "AAAAIAA", "DDDAIAD", "AAIADAD" };
            var plan2 = new string[] { "AIAIAIA", "DADADA", "DDAADDAA" };
            var translator = new CoordinateTranslator();
            var drones = DroneCrewBuilder.Init()
                                .WithTranslator(translator)
                                .WithOrders(new List<string[]> { plan1, plan2 })
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
