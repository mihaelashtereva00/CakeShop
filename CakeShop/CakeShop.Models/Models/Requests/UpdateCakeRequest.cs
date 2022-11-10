using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.Models.Models.Requests
{
    public class UpdateCakeRequest
    {
        public int Id { get; set; }
        public int BakerId { get; set; }
        public string Topping { get; set; }
        public string Base { get; set; }
        public string Form { get; set; }
        public decimal Price { get; set; }
    }
}
