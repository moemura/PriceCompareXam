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
    public class ShopeeService
    {
        static readonly string _alamat = "https://shopee.co.id/search?keyword";

        public static async Task<IEnumerable<ProductViewModel>> GetData(string keyword)
        {
            var daftarBarang = new List<ProductViewModel>();

            var hasilCrawl = await HttpService.Client.GetAsync(_alamat + keyword);
            if (hasilCrawl.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var message = await hasilCrawl.Content.ReadAsStringAsync();

                var parser = new HtmlParser();
                var document = await parser.ParseDocumentAsync(message);

                var cekJmlElemen = document.QuerySelectorAll(".shopee-search-item-result__items")
                    .Select(x => x);

                var productsContainer = document
                ?.QuerySelectorAll("shopee-search-item-result__items");

                foreach (var item in productsContainer)
                {
                    var daftarSementara = productsContainer
                        ?.Select(x => new ProductViewModel()
                        {
                            ImageUrl = x?.QuerySelector("img")?.GetAttribute("src") ?? string.Empty,
                            PageUrl = x?.QuerySelector("a")?.GetAttribute("href") ?? string.Empty,
                            WebSiteName = "Shopee",
                            Name = x?.QuerySelector("img")?.GetAttribute("alt") ?? string.Empty,
                            Detail = string.Empty,
                            Price = GetPrice(x),
                        });
                }
            }

            return daftarBarang.Where(x => x.Price != 0);
        }

        private static int GetPrice(IElement element)
        {
            return WebDataConvertHelper.WebPriceToNumber(element.QuerySelector("._24JoLh")?.InnerHtml ?? "");
        }
    }
}