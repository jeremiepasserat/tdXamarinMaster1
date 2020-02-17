using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;
using TD2.ViewModels;

namespace TD2.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        private LoginResult _loginResult;
        
        public MainPage(LoginResult loginResult)
        {
            InitializeComponent();
            _loginResult = loginResult;
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new MainPageViewModel(loginResult);
            
        }
        
        // Permet la maj d'affichage en cas de changement de profil
        protected override void OnAppearing()
        {
            
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new MainPageViewModel(_loginResult);
            base.OnAppearing();
        }
    }
}
