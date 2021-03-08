using DroneDelivery.Logic.Models;
using System.Collections.Generic;

namespace DroneDelivery.Logic
{
    public delegate void DroneNofity(DroneBase drone, string description);

    public class Drone : DroneBase, IDrone
    {
        public event DroneNofity OnNavigate;

        public event DroneNofity OnDeliver;

        public event DroneNofity OnStartDelivery;

        private readonly ICollection<DeliveryPlan> _deliveryPlans;

        public Drone(string key, ICollection<DeliveryPlan> deliveryPlans) : base(key)
        {
            _deliveryPlans = deliveryPlans;
        }

        public void Navigate()
        {
            int deliverNumber = 0;
            foreach (var delivery in _deliveryPlans)
            {
                OnStartDelivery?.Invoke(this, delivery.Address);
                foreach (var coordinate in delivery.Coordinates)
                {
                    OnNavigate?.Invoke(this, coordinate.ToString());
                }
            }
            OnDeliver?.Invoke(this, Key);
        }
    }
}