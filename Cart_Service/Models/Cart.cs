using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_Service.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public List<MenuItem> MenuItems { get; set; }
    }
}
