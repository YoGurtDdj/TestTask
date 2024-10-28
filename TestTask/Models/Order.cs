using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class Order
    {
        public int? OrderId { get; set; }
        public double? Weight { get; set; }
        public string? District { get; set; }
        public string? DeliveryTime
        {
            get
            {
                return _deliveryTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                _deliveryTime = DateTime.Parse(value);
            }
        }
        private DateTime _deliveryTime;
    }
}
