using DroneDelivery.Logic.Models;
using System.Collections.Generic;
using System.Linq;

namespace DroneDelivery.Logic
{
    public delegate void DroneNofity(DroneBase drone, string description);
    public delegate void FinisDeliveriesNotify(DroneBase drone, ICollection<Coordinates> coordinates);

    public class Drone : DroneBase, IDrone
    {
        public event DroneNofity OnNavigate;

        public event DroneNofity OnDelivery;

        public event FinisDeliveriesNotify OnFinishAllDeliveries;

        public event DroneNofity OnStartDelivery;

        private readonly ICollection<DeliveryPlan> _deliveryPlans;

        public Drone(string key, ICollection<DeliveryPlan> deliveryPlans) : base(key)
        {
            _deliveryPlans = deliveryPlans;
        }

        public void Navigate()
        {
            var destinations = new List<Coordinates>();
            foreach (var delivery in _deliveryPlans)
            {
                OnStartDelivery?.Invoke(this, delivery.Address);
                foreach (var coordinate in delivery.Coordinates)
                {
                    OnNavigate?.Invoke(this, coordinate.ToString());
                }

                var destinationCoordinates = delivery.Coordinates.Last();
                destinations.Add(destinationCoordinates);
                OnDelivery?.Invoke(this, destinationCoordinates.ToString());
            }

            OnFinishAllDeliveries?.Invoke(this, destinations);
        }
    }
}