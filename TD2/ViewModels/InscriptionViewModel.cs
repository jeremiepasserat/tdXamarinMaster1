using System;
using System.Windows.Input;
using Storm.Mvvm;
using Xamarin.Forms;

using TD2.Pages;
using System.Net.Http;
using Common.Api.Dtos;
using Newtonsoft.Json;
using TD.Api.Dtos;

namespace TD2.ViewModels 
{
    class InscriptionViewModel : ViewModelBase
    {
        public ICommand GoToConnexionCommand { get; }

        private string _prenomInsc;
        public string PrenomInsc
        {
            get => _prenomInsc;
            set => SetProperty(ref _prenomInsc, value);
        }

        private string _nomInsc;
        public string NomInsc
        {
            get => _nomInsc;
            set => SetProperty(ref _nomInsc, value);
        }

        private string _mailInsc;
        public string MailInsc
        {
            get => _mailInsc;
            set => SetProperty(ref _mailInsc, value);
        }

        private string _mdpInsc;
        public string MdpInsc
        {
            get => _mdpInsc;
            set => SetProperty(ref _mdpInsc, value);
        }



        public InscriptionViewModel()
        {
            GoToConnexionCommand = new Command(GoToConnectionAction);
        }

        private async void GoToConnectionAction()
        {
            // appel à l'API pour charger l'utilisateur

            if (string.IsNullOrWhiteSpace(_prenomInsc) || string.IsNullOrWhiteSpace(_nomInsc) || string.IsNullOrWhiteSpace(_mailInsc) || string.IsNullOrWhiteSpace(_mdpInsc))
                System.Diagnostics.Debug.WriteLine("Merci de renseigner les informations demandées");

            else
            {
                   System.Diagnostics.Debug.WriteLine("Prenom : " + _prenomInsc);
                   System.Diagnostics.Debug.WriteLine("Nom : " + _nomInsc);
                   System.Diagnostics.Debug.WriteLine("Mail : " + _mailInsc);
                   System.Diagnostics.Debug.WriteLine("Mdp : " + _mdpInsc);

                   ApiClient api = new ApiClient();
                   


                   RegisterRequest newUser = new RegisterRequest();
                   newUser.Email = _mailInsc;
                   newUser.FirstName = _mailInsc;
                   newUser.LastName = _mailInsc;
                   newUser.Password = _mdpInsc;


                   HttpResponseMessage reponse = await api.Execute(HttpMethod.Post, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["UrlRegister"], newUser, null);
                   LoginResult loginResult = new LoginResult();
                   Response<LoginResult> resp = new Response<LoginResult>();
                   
                   if (reponse.IsSuccessStatusCode) {
                       resp = await api.ReadFromResponse<Response<LoginResult>>(reponse);
                       string token = resp.Data.AccessToken;
                       loginResult = resp.Data;
                
                       //System.Diagnostics.Debug.WriteLine("Le token de connexion est : " + loginResult.AccessToken);
                       await NavigationService.PushAsync(new MainPage(loginResult));
                   }

                   else
                   {
                       System.Diagnostics.Debug.WriteLine("Problème dans la requête de connexion : " + resp.ErrorMessage);
                   }

    
            }




        }

    }
}
