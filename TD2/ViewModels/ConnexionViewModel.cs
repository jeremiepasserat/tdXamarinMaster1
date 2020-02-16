using System;
using System.Net.Http;
using System.Windows.Input;
using Common.Api.Dtos;
using Newtonsoft.Json;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;

using TD2.Pages;
using Xamarin.Forms.Internals;

namespace TD2.ViewModels
{
    class ConnexionViewModel : ViewModelBase
    {
        public ICommand GoToMainCommand { get; }

        
        private string _idApp;
        public string IdApp
        {
            get => _idApp;
            set => SetProperty(ref _idApp, value);
        }

        private string _mdpApp;
        public string MdpApp
        {
            get => _mdpApp;
            set => SetProperty(ref _mdpApp, value);
        }
        
        public ConnexionViewModel()
        {
            GoToMainCommand = new Command(GoToMainPageAction);
        }

        private async void GoToMainPageAction()
        {
            ApiClient api = new ApiClient();
            

            LoginRequest loginRequest = new LoginRequest();
            loginRequest.Email = _idApp;
            loginRequest.Password = _mdpApp;


            HttpResponseMessage reponse = await api.Execute(HttpMethod.Post, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["UrlLogin"], loginRequest, null);

            LoginResult loginResult = new LoginResult();
            Response<LoginResult> resp = new Response<LoginResult>();

            
            if (reponse.IsSuccessStatusCode) {
                resp = await api.ReadFromResponse<Response<LoginResult>>(reponse);
                string token = resp.Data.AccessToken;
                //Console.WriteLine("Token : " + token);
                loginResult = resp.Data;
                
                await NavigationService.PushAsync(new MainPage(loginResult));

            }

            else
            {
                System.Diagnostics.Debug.WriteLine("Problème dans la requête de connexion : " + resp.ErrorMessage);

                
            }
            
            

        }
    }
}
