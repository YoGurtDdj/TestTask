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
            try { 
                var orders = JsonData.Desirialize<Order>().AsQueryable();

                if (!string.IsNullOrWhiteSpace(filter.CityDistrict))
                {
                    Logger.WriteToLog("Filtering data...");

                    orders = orders.Where(i => i.District.Contains(filter.CityDistrict));
                    JsonData.Serialize(orders.ToList());
                }
                if (filter.FromDeliveryTime != null || filter.ToDeliveryTime != null)
                {
                    Logger.WriteToLog("Filtering data...");

                    orders = orders.Where(i => DateTime.Parse(i.DeliveryTime) >= filter.FromDeliveryTime && DateTime.Parse(i.DeliveryTime) <= filter.ToDeliveryTime);
                    JsonData.Serialize(orders.ToList());
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
    }
}
