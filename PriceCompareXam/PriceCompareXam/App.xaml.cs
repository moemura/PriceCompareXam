using PriceCompareXam.Services;
using PriceCompareXam.Views;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceCompareXam
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            CultureInfo.CurrentCulture = new CultureInfo("id-ID");
            CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
