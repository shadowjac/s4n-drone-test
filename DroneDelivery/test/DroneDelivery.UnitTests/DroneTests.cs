using DroneDelivery.Logic;
using DroneDelivery.Logic.Models;
using DroneDelivery.Logic.Models.Enums;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DroneDelivery.UnitTests
{
    public class DroneTests
    {
        [Fact]
        public async Task CreateDrone_WhenSendAKeyInConstructor_ShouldBeSettedProperly()
        {
            var drone = new Drone("drone 1", null);
            drone.Key.Should().Be("drone 1");
        }

        [Fact]
        public async Task CreatingDrone_WhenNavigateWithoutDeliveryPlan_ShouldNotRaiseAnyEvent()
        {
            var eventRaised = false;
            var drone = new Drone("drone 1", null);
            drone.OnNavigate += (s, arg) => eventRaised = true;
            drone.Navigate();

            drone.Key.Should().Be("drone 1");
            eventRaised.Should().BeFalse();
        }

        [Fact]
        public async Task CreatingDrone_WhenNavigateDeliveryPlan_ShouldRaiseAllEvents()
        {
            var deliveryPlans = new List<DeliveryPlan>()
            {
                new DeliveryPlan{ Address ="St1", Coordinates = new Coordinates[]{new Coordinates{ X = 0, Y = 0, Orientation = Orientation.N } } },
                new DeliveryPlan{ Address ="St2", Coordinates = new Coordinates[]{new Coordinates{ X = 0, Y = 1, Orientation = Orientation.N } } },
                new DeliveryPlan{ Address ="St3", Coordinates = new Coordinates[]{new Coordinates{ X = 1, Y = 1, Orientation = Orientation.E } } }
            };
            var drone = new Drone("drone 1", deliveryPlans);
            bool onNavigateRaised = false, onStartRaised = false, onFinishRaised = false, onDeliveryRaised = false;

            drone.OnNavigate += (s, arg) => onNavigateRaised = true;
            drone.OnStartDelivery += (s, arg) => onStartRaised = true;
            drone.OnFinishAllDeliveries += (s, arg) => onFinishRaised = true;
            drone.OnDelivery += (s, arg) => onDeliveryRaised = true;
            drone.Navigate();

            drone.Key.Should().Be("drone 1");
            onNavigateRaised.Should().BeTrue();
            onStartRaised.Should().BeTrue();
            onFinishRaised.Should().BeTrue();
            onDeliveryRaised.Should().BeTrue();
        }

        [Fact]
        public async Task CreatingDrone_WhenFinishNavigation_ShouldRaiseFinishEventWithDeliveryCoordinatesOnlyAsArgs()
        {
            var deliveryPlans = new List<DeliveryPlan>()
            {
                new DeliveryPlan
                {
                    Address ="St1",
                    Coordinates = new Coordinates[]
                    {
                        new Coordinates{ X = 0, Y = 0, Orientation = Orientation.N },
                        new Coordinates { X = 0, Y = 5, Orientation = Orientation.S } 
                    }
                },
                new DeliveryPlan
                {
                    Address ="St2", 
                    Coordinates = new Coordinates[]
                    {
                        new Coordinates{ X = 0, Y = 1, Orientation = Orientation.N },
                        new Coordinates { X = 0, Y = 0, Orientation = Orientation.W }
                    }
                },
            };
            var drone = new Drone("drone 1", deliveryPlans);
            IList<Coordinates> deliveryCoordinates = null;

            drone.OnFinishAllDeliveries += (_, arg) => deliveryCoordinates = arg.ToList();
            drone.Navigate();

            drone.Key.Should().Be("drone 1");
            deliveryCoordinates.Should().NotBeNull();
            deliveryCoordinates.Should().NotBeEmpty();
            deliveryCoordinates.Count.Should().Be(2);
            deliveryCoordinates[0].Orientation.Should().Be(Orientation.S);
            deliveryCoordinates[0].X.Should().Be(0);
            deliveryCoordinates[0].Y.Should().Be(5);
            deliveryCoordinates[1].Orientation.Should().Be(Orientation.W);
            deliveryCoordinates[1].X.Should().Be(0);
            deliveryCoordinates[1].Y.Should().Be(0);

        }
    }
}