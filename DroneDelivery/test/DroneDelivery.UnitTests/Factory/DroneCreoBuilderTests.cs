using DroneDelivery.Logic.Factory;
using DroneDelivery.Logic.IO;
using DroneDelivery.Logic.Models;
using DroneDelivery.Logic.Translator;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DroneDelivery.UnitTests.Factory
{
    public class DroneCreoBuilderTests
    {
        [Fact]
        public async Task DroneCrewBuilder_WhenBuildWithBasicConfiguration_ShouldReturnEmptyDroneCrew()
        {
            var result = DroneCrewBuilder.Init()
                              .Build();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task DroneCrewBuilder_WhenBuildWithDeliveryPlanLoader_ShouldReturnAListOfDronesWithTheyDeliveryPlans()
        {
            var result = DroneCrewBuilder.Init()
                              .WithTranslator(new CoordinateTranslator())
                              .WithOrderLoader(new TestLoader())
                              .Build().ToList();
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count.Should().Be(2);
            
            result[0].Key.Should().Be("1");
            result[0].DeliveryPlans.Count().Should().Be(3);
            result[0].DeliveryPlans.All(p => p.Coordinates.Count() == 5).Should().BeTrue();

            result[1].Key.Should().Be("2");
            result[1].DeliveryPlans.Count().Should().Be(3);
            result[1].DeliveryPlans.All(p => p.Coordinates.Count() == 4).Should().BeTrue();
        }

        [Fact]
        public async Task DroneCrewBuilder_WhenBuildWithDeliveryPlanLoaderAndFinishEventAttachedAndCallNavigateFunction_ShouldNotifyWhenAllDeliveriesWereFinishedAndTheirCoordinates()
        {
            var deliveryCoordinates = new List<Coordinates>(0);
            var result = DroneCrewBuilder.Init()
                              .WithTranslator(new CoordinateTranslator())
                              .WithFinishAllDeliveriesNotification((s, arg) => deliveryCoordinates.AddRange(arg))
                              .WithOrderLoader(new TestLoader())
                              .Build().ToList();
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count.Should().Be(2);

            result[0].Key.Should().Be("1");
            result[0].DeliveryPlans.Count().Should().Be(3);
            result[0].DeliveryPlans.All(p => p.Coordinates.Count() == 5).Should().BeTrue();

            result[1].Key.Should().Be("2");
            result[1].DeliveryPlans.Count().Should().Be(3);
            result[1].DeliveryPlans.All(p => p.Coordinates.Count() == 4).Should().BeTrue();

            result.ForEach(drone => drone.Navigate());
            deliveryCoordinates.Should().NotBeNull();
            deliveryCoordinates.Should().NotBeEmpty();
            deliveryCoordinates.Count.Should().Be(6);

            deliveryCoordinates[0].ToString().Should().Be("(-1, 3), Direction West");
            deliveryCoordinates[1].ToString().Should().Be("(-1, 3), Direction North");
            deliveryCoordinates[2].ToString().Should().Be("(1, 3), Direction North");
            deliveryCoordinates[3].ToString().Should().Be("(1, 2), Direction North");
            deliveryCoordinates[4].ToString().Should().Be("(1, -2), Direction South");
            deliveryCoordinates[5].ToString().Should().Be("(-1, 0), Direction South");
        }
    }

    internal class TestLoader : ILoader<IEnumerable<IEnumerable<string>>>
    {
        public IEnumerable<IEnumerable<string>> LoadInfo() => new List<string[]>
        {
            new []{ "AAAI","AIDA","DIAA"},
            new []{ "ADI","DDA","AII"}
        };
    }
}