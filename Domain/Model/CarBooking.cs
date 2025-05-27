using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class CarBooking
    {
        public Guid ID { get; set; }
        public Guid userID { get; set; }
        public virtual User user { get; set; }
        public Guid carID { get; set; }
        public virtual Car car { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartingfromDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int Price { get; set; }

        


    }
}
