using PriceCompareXam.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PriceCompareXam.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}