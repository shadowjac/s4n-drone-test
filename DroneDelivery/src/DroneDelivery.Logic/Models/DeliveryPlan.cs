using System.Collections.Generic;

namespace DroneDelivery.Logic.Models
{
    public class DeliveryPlan
    {
        public string Address { get; set; }
        public IEnumerable<Coordinates> Coordinates{ get; set; }
    }
}
