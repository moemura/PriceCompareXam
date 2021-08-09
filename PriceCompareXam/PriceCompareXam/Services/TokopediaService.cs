using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using PriceCompareXam.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCompareXam.Services
{
    public class TokopediaService
    {
        static readonly string _alamat = "https://www.tokopedia.com/search?st=product&q=";

        public static async Task<IEnumerable<ProductViewModel>> GetData(string keyword)
        {
            var daftarBarang = new List<ProductViewModel>();

            var hasilCrawl = await HttpService.Client.GetAsync(_alamat + keyword);
            if (hasilCrawl.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var message = await hasilCrawl.Content.ReadAsStringAsync();

                message = message.Replace("data-testid=\"divSRPContentProducts\"", "class=\"divSRPContentProducts\"");

                var parser = new HtmlParser();
                var document = await parser.ParseDocumentAsync(message);

                var cekJmlElemen = document.QuerySelectorAll(".divSRPContentProducts")
                    .Select(x => x);

                var productsContainer = document
                ?.QuerySelectorAll(".divSRPContentProducts");

                foreach (var item in productsContainer)
                {
                    var daftarSementara = item.QuerySelectorAll(".css-12sieg3")
                        ?.Select(x => new ProductViewModel()
                        {
                            ImageUrl = x?.QuerySelector(".css-1ehqh5q")?.QuerySelector("img")?.GetAttribute("src") ?? string.Empty,
                            PageUrl = x?.QuerySelector(".css-1ehqh5q")?.QuerySelector("a")?.GetAttribute("href") ?? string.Empty,
                            WebSiteName = "Tokopedia",
                            Name = x?.QuerySelector(".css-7fmtuv")?.QuerySelector("a")?.GetAttribute("title"),
                            Detail = string.Empty,
                            Price = GetPrice(x),
                        });

                    //filter iklan
                    daftarBarang.AddRange(daftarSementara.Where(x => !x.PageUrl.Contains("ta.tokopedia")));
                }
            }

            return daftarBarang.Where(x => x.Price != 0);
        }

        private static int GetPrice(IElement element)
        {
            return WebDataConvertHelper.WebPriceToNumber(element.QuerySelector(".css-rhd610")?.InnerHtml ?? "");
        }
    }
}