using DroneDelivery.Logic.Infrastructure;
using DroneDelivery.Logic.Models;
using System.Collections.Generic;
using System.Linq;

namespace DroneDelivery.Logic
{
    public class Drone : DroneBase, IDrone
    {
        public event Delegates.DroneNofity OnNavigate;

        public event Delegates.DroneNofity OnDelivery;

        public event Delegates.FinisDeliveriesNotify OnFinishAllDeliveries;

        public event Delegates.DroneNofity OnStartDelivery;

        private readonly IEnumerable<DeliveryPlan> _deliveryPlans;

        public Drone(string key, IEnumerable<DeliveryPlan> deliveryPlans) : base(key)
        {
            _deliveryPlans = deliveryPlans;
        }

        public void Navigate()
        {
            if (_deliveryPlans == null) return;
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