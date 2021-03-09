using DroneDelivery.Logic.Models;
using System.Collections.Generic;

namespace DroneDelivery.Logic.Infrastructure
{
    public class Delegates
    {
        public delegate void DroneNofity(DroneBase drone, string description);

        public delegate void FinisDeliveriesNotify(DroneBase drone, ICollection<Coordinates> coordinates);
    }
}