using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caTest
{
    public class Promotion
    {
 

        public string item { get; set; }
        public decimal OriginPrice { get; set; }
        public string PromotionType { get; set; }
        public decimal Param { get; set; }
        public decimal FinalPrice => calculateFinal();

        public Promotion()
        {

        }

        public Promotion(string items, decimal originPrice, string promotionType, decimal param)
        {
            item = items;
            OriginPrice = originPrice;
            PromotionType = promotionType;
            Param = param;
        }

        public decimal calculateFinal()
        {
            var result = PromotionType == "F" ? Param : OriginPrice * Param / 100;

            return result;
        }

    }
}
