using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeShop.Models.Models.ModelsSqlDB;

namespace CakeShop.Models.Models.Responses.BakerResponses
{
    public class GetAllBakersResponse : BaseResponse
    {
        public IEnumerable<Baker> Bakers { get; set; }
    }
}
