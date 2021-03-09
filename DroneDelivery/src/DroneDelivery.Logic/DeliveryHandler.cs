using DroneDelivery.Logic.Factory;
using DroneDelivery.Logic.IO;
using DroneDelivery.Logic.Translator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DroneDelivery.Logic
{
    public static class DeliveryHandler
    {
        private static ICollection<Drone> _drones;

        static DeliveryHandler()
        {
            _drones = DroneCrewBuilder.Init()
                              .WithTranslator(new CoordinateTranslator())
                              .WithOrderLoader(new FileLoader(@"D:\s4n"))
                              .WithDeliveryNotification((s, msg) => Console.WriteLine($"Drone {s.Key} delivered at {msg}"))
                              .WithStartNavigationNotification((s, msg) => Console.WriteLine($"Drone {s.Key} starting delivery to {msg}"))
                              .WithFinishAllDeliveriesNotification((currentDrone, deliveries) =>
                              {
                                  var writer = new FileWriter(@$"D:\s4n\output\out{currentDrone.Key}.txt");
                                  var sb = new StringBuilder($"*** Reporte de entregas para dron: {currentDrone.Key} ***{Environment.NewLine}");
                                  foreach (var coordinate in deliveries)
                                  {
                                      sb.AppendLine(coordinate.ToString());
                                  }
                                  writer.Write(sb.ToString());
                              })
                              .Build();
        }

        public static void StartAsync()
        {
            var tasks = new List<Task>();

            foreach (var drone in _drones)
            {
                tasks.Add(Task.Factory.StartNew(drone.Navigate));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public static void Start()
        {
            foreach (var drone in _drones)
            {
                drone.Navigate();
            }
        }
    }
}