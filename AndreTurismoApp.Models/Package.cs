using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Models
{
    public class Package
    {
        public int Id { get; set; }
        public Hotel IdHotel { get; set; }
        public Ticket IdTicket { get; set; }
        public Customer IdCustomer { get; set; }
        public decimal Value { get; set; }
    }
}
