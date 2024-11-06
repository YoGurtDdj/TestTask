using Microsoft.AspNetCore.Mvc;
using TestTask.Models;
using TestTask.Helpers;
using System.Net;



namespace TestTask.Controllers
{
    [Route("tt/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll([FromQuery] FilteringParameters filter)
        {
            try 
            { 
                var orders = JsonData.Deserialize<Order>("data.json").AsQueryable();

                if (!string.IsNullOrWhiteSpace(filter.CityDistrict))
                {
                    Logger.WriteToLog("Filtering data...");

                    orders = orders.Where(i => i.District.Contains(filter.CityDistrict));
                    JsonData.Serialize(orders.ToList(), "result.json");
                }
                if (filter.FromDeliveryTime != null || filter.ToDeliveryTime != null)
                {
                    Logger.WriteToLog("Filtering data...");

                    orders = orders.Where(i => DateTime.Parse(i.DeliveryTime) >= filter.FromDeliveryTime && DateTime.Parse(i.DeliveryTime) <= filter.ToDeliveryTime);
                    JsonData.Serialize(orders.ToList(), "result.json");
                }

                Logger.WriteToLog("Returning data");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Exception: " + ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            Logger.WriteToLog("Posting data to data.json... ");
            try
            {
                List<Order> orders = [order];
                JsonData.Serialize(orders, "data.json");

                Logger.WriteToLog("Data posted");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Exception: " + ex);
                throw new Exception("Ошибка: " + ex.Message);
            }
        }
    }
}
