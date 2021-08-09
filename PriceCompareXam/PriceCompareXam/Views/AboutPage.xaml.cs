using PriceCompareXam.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceCompareXam.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = this.BindingContext as AboutViewModel;
            vm.ItemTappedCommand.Execute(listView.SelectedItem);
        }
    }
}