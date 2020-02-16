using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TD2.Pages;
using Storm.Mvvm;

namespace TD2
{
    public partial class App : MvvmApplication
    {
        public App() : base(()=>new FirstPage())
        {
            InitializeComponent();

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
