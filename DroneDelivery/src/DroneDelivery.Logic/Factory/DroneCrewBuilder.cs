using DroneDelivery.Logic.Infrastructure;
using DroneDelivery.Logic.IO;
using DroneDelivery.Logic.Models;
using DroneDelivery.Logic.Translator;
using System.Collections.Generic;
using System.Linq;

namespace DroneDelivery.Logic.Factory
{
    public sealed class DroneCrewBuilder
    {
        private ITranslator<IEnumerable<string>, IEnumerable<DeliveryPlan>> _translator;
        private IEnumerable<IEnumerable<string>> _orders;
        private Delegates.DroneNofity _navigationDelegate;
        private Delegates.DroneNofity _startNavigationDelegate;
        private Delegates.DroneNofity _deliveryDelegate;
        private Delegates.FinisDeliveriesNotify _allDeliveriesNotificationDelegate;

        private DroneCrewBuilder()
        {
        }

        public static DroneCrewBuilder Init() => new DroneCrewBuilder();

        public DroneCrewBuilder WithTranslator(ITranslator<IEnumerable<string>, IEnumerable<DeliveryPlan>> translator)
        {
            _translator = translator;
            return this;
        }

        public DroneCrewBuilder WithOrders(IEnumerable<IEnumerable<string>> orders)
        {
            _orders = orders;
            return this;
        }

        public DroneCrewBuilder WithNavigateNotification(Delegates.DroneNofity navigationDelegate)
        {
            _navigationDelegate = navigationDelegate;
            return this;
        }

        public DroneCrewBuilder WithStartNavigationNotification(Delegates.DroneNofity startNavigationDelegate)
        {
            _startNavigationDelegate = startNavigationDelegate;
            return this;
        }

        public DroneCrewBuilder WithDeliveryNotification(Delegates.DroneNofity deliveryDelegate)
        {
            _deliveryDelegate = deliveryDelegate;
            return this;
        }

        public DroneCrewBuilder WithFinishAllDeliveriesNotification(Delegates.FinisDeliveriesNotify finisDeliveriesNotifyDelegate)
        {
            _allDeliveriesNotificationDelegate = finisDeliveriesNotifyDelegate;
            return this;
        }

        public DroneCrewBuilder WithOrderLoader(ILoader<IEnumerable<IEnumerable<string>>> loader)
        {
            _orders = loader.LoadInfo();
            return this;
        }

        public ICollection<Drone> Build()
        {
            var drones = new List<Drone>(_orders.Count());
            int index = 0;
            foreach (var ordersByDrone in _orders)
            {
                var drone = new Drone($"{++index}", _translator.Translate(ordersByDrone));
                drone.OnNavigate += _navigationDelegate;
                drone.OnStartDelivery += _startNavigationDelegate;
                drone.OnDelivery += _deliveryDelegate;
                drone.OnFinishAllDeliveries += _allDeliveriesNotificationDelegate;
                drones.Add(drone);
            }
            return drones;
        }
    }
}