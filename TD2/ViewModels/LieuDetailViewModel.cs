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

        private ApiClient _api = new ApiClient();

        private string _accessToken;

        public PlaceItemSummary Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
        }

        public LieuDetailViewModel(PlaceItemSummary lieuChoisi, string loginResultAccessToken)
        {
            _place = lieuChoisi;
            _accessToken = loginResultAccessToken;

            //throw new NotImplementedException();
        }
    }

}