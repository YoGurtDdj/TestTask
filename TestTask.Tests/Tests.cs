using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TestTask.Controllers;
using TestTask.Helpers;
using TestTask.Models;
using System.Collections.Generic;
using System.Linq;



namespace TestTask.Tests
{
    public class Tests
    {
        [Fact]
        public void GetAll_ReturnsFilteredOrders_ByCityDistrict()
        {
            // Arrange
            var mockJsonData = new Mock<IJsonData>();
            mockJsonData.Setup(j => j.Deserialize<Order>()).Returns(GetSampleOrders());

            var controller = new OrderController(mockJsonData.Object);
            var filter = new FilteringParameters { CityDistrict = "Downtown" };

            // Act
            var result = controller.GetAll(filter) as OkObjectResult;
            var filteredOrders = result.Value as List<Order>;

            // Assert
            Assert.NotNull(filteredOrders);
            Assert.All(filteredOrders, o => Assert.Contains("Downtown", o.District));
        }


        private List<Order> GetSampleOrders()
        {
            return new List<Order>
        {
            new Order { OrderId = 1, District = "Downtown", DeliveryTime = "2023-08-01 10:30:00" },
            new Order { OrderId = 2, District = "Suburbs", DeliveryTime = "2023-08-01 11:00:00" }
        };
        }
    }
}