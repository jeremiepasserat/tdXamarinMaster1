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
    public class LieuDetailViewModel : ViewModelBase
    {
        private PlaceItemSummary _place;

        private PlaceItem _placeDetail;

        private ApiClient _api = new ApiClient();

        private string _accessToken;

        public ICommand GoToMapCommand { get; }

        public PlaceItemSummary Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
        }

        public PlaceItem PlaceDetail
        {
            get => _placeDetail;
            set => SetProperty(ref _placeDetail, value);
        }

        public LieuDetailViewModel(PlaceItemSummary lieuChoisi, string loginResultAccessToken)
        {
            _place = lieuChoisi;
            _accessToken = loginResultAccessToken;
            
            GoToMapCommand = new Command(ChargerLieuSurCarte);

            chargerDetailLieu();

            //throw new NotImplementedException();
        }

        private async void chargerDetailLieu()
        {

            HttpResponseMessage reponse = await _api.Execute(HttpMethod.Get, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["Places"] + "/" + Place.Id, null, _accessToken);
            Response<PlaceItem> resp = new Response<PlaceItem>();
                       
            //System.Diagnostics.Debug.WriteLine("Le statut de connexion est : " + reponse.StatusCode.ToString());
                       
            if (reponse.IsSuccessStatusCode) {
                resp = await _api.ReadFromResponse<Response<PlaceItem>>(reponse);

                PlaceDetail = resp.Data;
             
            }
           
            else
            {
                System.Diagnostics.Debug.WriteLine("Problème dans la requête de connexion : " + resp.ErrorMessage);
            }
            
            
            
        }

        public async void ChargerLieuSurCarte()
        {

            await NavigationService.PushAsync(new CartePage(_place));
        }
        
    }

}