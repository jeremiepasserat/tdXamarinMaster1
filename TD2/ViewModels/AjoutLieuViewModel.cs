using System;
using System.Net.Http;
using System.Windows.Input;
using Common.Api.Dtos;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using TD.Api.Dtos;
using TD2.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class AjoutLieuViewModel :  ViewModelBase
    {
        private string _accessToken;
        //private ImageItem _newImage;
        private int _idData;

        private string _titreLieu;
        private string _descriptionLieu;
        private bool _useGps;

        public string TitreLieu
        {
            get => _titreLieu;
            set => SetProperty(ref _titreLieu, value);
        }

        public string DescriptionLieu
        {
            get => _descriptionLieu;
            set => SetProperty(ref _descriptionLieu, value);
        }

        public bool UseGps
        {
            get => _useGps;
            set => SetProperty(ref _useGps, value);
        }

        private string _longitude;
        private string _latitude;

        public ICommand PublishCommand { get; }

        public string LatitudeLieu
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        public string LongitudeLieu
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }


        public AjoutLieuViewModel(string accessToken, int respData)
        {
            _accessToken = accessToken;
            _idData = respData;
            UseGps = true;
            PublishCommand = new Command(AjouterLieu);
            //System.Diagnostics.Debug.WriteLine("L'id de l'image ajoutée est : " + _idData);

            //AjouterLieu();
            //throw new System.NotImplementedException();
        }

        private async void AjouterLieu()
        {
            ApiClient api = new ApiClient();
            
            CreatePlaceRequest placeRequest = new CreatePlaceRequest();
            placeRequest.ImageId = _idData;
            placeRequest.Description = _descriptionLieu;
            placeRequest.Title = _titreLieu;
            
        /*    System.Diagnostics.Debug.WriteLine("Titre : " + TitreLieu);
            System.Diagnostics.Debug.WriteLine("Description : " + DescriptionLieu);
            System.Diagnostics.Debug.WriteLine("Image : " + _idData);

            System.Diagnostics.Debug.WriteLine("GPS ? " + _useGps);
            
            System.Diagnostics.Debug.WriteLine("URL ? " + (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["Places"]);
*/
        System.Diagnostics.Debug.WriteLine("Oh beybe beybe");

        
            if (UseGps) {
            
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    placeRequest.Latitude = location.Latitude;
                    placeRequest.Longitude = location.Longitude;
                }
                
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Données manouelles");
                placeRequest.Latitude = double.Parse(_latitude);
                placeRequest.Longitude = double.Parse(_longitude);
            }
            
            
            
            HttpResponseMessage reponse = await api.Execute(HttpMethod.Post, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["Places"], placeRequest, _accessToken);

         
            System.Diagnostics.Debug.WriteLine("Response " + reponse);

            if (reponse.IsSuccessStatusCode)
            {
                // On revient sur la page principale
                await NavigationService.PopAsync();
                await NavigationService.PopAsync();

            }

            // LoginResult loginResult = new LoginResult();
            // Response<LoginResult> resp = new Response<LoginResult>();

         
         
            /*
            if (reponse.IsSuccessStatusCode) {
                resp = await api.ReadFromResponse<Response<LoginResult>>(reponse);
                string token = resp.Data.AccessToken;
                //Console.WriteLine("Token : " + token);
                loginResult = resp.Data;
                
                System.Diagnostics.Debug.WriteLine("Ajout Lieu ok : " + resp);
   
                
                //await NavigationService.PushAsync(new MainPage(loginResult));
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Problème dans la requête de connexion : " + resp.ErrorMessage);
                
            }*/
            // throw new System.NotImplementedException();
        }
    }
}