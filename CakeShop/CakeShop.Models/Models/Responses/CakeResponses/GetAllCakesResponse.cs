using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeShop.Models.Models.ModelsSqlDB;

namespace CakeShop.Models.Models.Responses.CakeResponses
{
    public class GetAllCakesResponse : BaseResponse
    {
        public IEnumerable<Cake> Cakes { get; set; }
    }
}
