using System;
using System.Windows.Input;
using Storm.Mvvm;
using Xamarin.Forms;

using TD2.Pages;

namespace TD2.ViewModels
{
    class FirstPageViewModel : ViewModelBase
    {
        public ICommand GoToConnectionCommand { get; }
        public ICommand GoToInscriptionCommand { get; }


        public FirstPageViewModel()
        {
            GoToConnectionCommand = new Command(GoToConnectionAction);
            GoToInscriptionCommand = new Command(GoToInscriptionAction);

        }

        private async void GoToConnectionAction()
        {
            await NavigationService.PushAsync(new ConnexionPage());
        }

        private async void GoToInscriptionAction() => await NavigationService.PushAsync(new InscriptionPage());

    }
}
