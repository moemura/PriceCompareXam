using System;
using System.Collections.Generic;
using System.Text;

namespace PriceCompareXam.Services
{
    class WebDataConvertHelper
    {
        public static int WebPriceToNumber(string priceText)
        {
            var filter = priceText.Replace("Rp", "").Replace(".", "");
            int.TryParse(filter, out int hasil);
            return hasil;
        }
    }
}
