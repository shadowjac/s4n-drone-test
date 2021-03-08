using DroneDelivery.Logic.IO;
using DroneDelivery.Logic.Models;
using DroneDelivery.Logic.Translator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DroneDelivery.Logic.Factory
{
    public sealed class DroneCrewBuilder
    {
        private ITranslator<IEnumerable<string>, ICollection<DeliveryPlan>> _translator;
        private IEnumerable<IEnumerable<string>> _orders;
        private DroneNofity _navigationDelegate;
        private DroneNofity _startNavigationDelegate;

        private DroneCrewBuilder()
        {
        }

        public static DroneCrewBuilder Init() => new DroneCrewBuilder();

        public DroneCrewBuilder WithTranslator(ITranslator<IEnumerable<string>, ICollection<DeliveryPlan>> translator)
        {
            _translator = translator;
            return this;
        }

        public DroneCrewBuilder WithOrders(IEnumerable<IEnumerable<string>> orders)
        {
            _orders = orders;
            return this;
        }

        public DroneCrewBuilder WithNavigateNotification(DroneNofity navigationDelegate)
        {
            _navigationDelegate = navigationDelegate;
            return this;
        }

        public DroneCrewBuilder WithStartNavigationNotification(DroneNofity startNavigationDelegate)
        {
            _startNavigationDelegate = startNavigationDelegate;
            return this;
        }

        public DroneCrewBuilder WithOrderLoader(ILoader<IEnumerable<IEnumerable<string>>> loader)
        {
            _orders = loader.LoadInfoAsync().GetAwaiter().GetResult();
            return this;
        }

        public ICollection<Drone> Build()
        {
            var drones = new List<Drone>(_orders.Count());
            int index = 0;
            foreach (var ordersByDrone in _orders)
            {
                var drone = new Drone($"drone{++index}", _translator.Translate(ordersByDrone));
                drone.OnNavigate += _navigationDelegate;
                drone.OnStartDelivery += _startNavigationDelegate;
                drones.Add(drone);
            }
            return drones;
        }
    }
}