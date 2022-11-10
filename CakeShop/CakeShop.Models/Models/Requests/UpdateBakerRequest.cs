using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.Models.Models.Requests
{
    public class UpdateBakerRequest
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public DateTime DateOfBirth { get; init; }
        public int Age { get; set; }
        public string Specialty { get; set; }
    }
}
