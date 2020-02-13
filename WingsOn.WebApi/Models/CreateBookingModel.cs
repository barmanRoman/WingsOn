using System.Collections.Generic;

namespace WingsOn.WebApi.Models
{
    public class CreateBookingModel
    {
        public string Number { get; set; }
        public int Customer { get; set; }
        public int Flight { get; set; }
        public IEnumerable<int> Passengers { get; set; }
    }
}