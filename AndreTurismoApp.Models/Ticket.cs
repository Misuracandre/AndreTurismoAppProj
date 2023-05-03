using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Address IdOrigin { get; set; }
        public Address IdDestination { get; set; }
        public Customer IdCustomer { get; set; }
        public decimal Value { get; set; }
    }
}
