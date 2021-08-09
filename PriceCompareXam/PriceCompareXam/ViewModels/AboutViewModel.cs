using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using PriceCompareXam.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PriceCompareXam.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            CekHargaCommand = new Command(async () => await GetElement());
            ItemTappedCommand = new Command(async (obj) => await ItemTappedAction(obj));
        }

        public ICommand CekHargaCommand { get; }
        public ICommand ItemTappedCommand { get; }

        ObservableCollection<ProductViewModel> _listOfBarang = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProductViewModel> ListOfBarang { get { return _listOfBarang; } }

        private string _keyword;

        public string Keyword
        {
            get => _keyword;
            set => SetProperty(ref _keyword, value);
        }

        private async Task GetElement()
        {
            try
            {
                var daftarBarangTokopedia = await TokopediaService.GetData(Keyword);
                var daftarBarangShopee = await ShopeeService.GetData(Keyword);

                _listOfBarang.Clear();
                foreach (var item in daftarBarangTokopedia)
                {
                    if (item.Price != 0)
                        _listOfBarang.Add(item);
                }

                foreach (var item in daftarBarangShopee)
                {
                    if (item.Price != 0)
                        _listOfBarang.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task ItemTappedAction(object obj)
        {
            await Browser.OpenAsync((obj as ProductViewModel).PageUrl);
        }
    }
}