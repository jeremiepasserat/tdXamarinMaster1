using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Storm.Mvvm;
using Xamarin.Forms;

using TD2.Pages;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Api.Dtos;
using Newtonsoft.Json;
using TD.Api.Dtos;

namespace TD2.ViewModels
{
    class MainPageViewModel : ViewModelBase 
    {
        
        public ICommand CreateLieuCommand { get; }
        public ICommand ChangeUserCommand { get; }
        
       // public ICommand  LoadOnePlaceCommand { get; }
       
       private String _titreMain;
        private UserItem _userItem = new UserItem();
        private ApiClient _api = new ApiClient();
        private LoginResult _loginResult;

        private ObservableCollection<PlaceItemSummary> _listeLieux;

        private String _urlId;

        public string UrlId
        {
            get => _urlId;
            set => SetProperty(ref _urlId, " https://td-api.julienmialon.com/images/" + value);
        }

        public ObservableCollection<PlaceItemSummary> ListeLieux
        {
            get => _listeLieux;
            set => SetProperty(ref _listeLieux, value);
        }

        private PlaceItemSummary _selectedLieu;
        public PlaceItemSummary SelectedLieu
        {
            get => _selectedLieu;
            //set => SetProperty(ref _selectedLieu, value);
            set {
                if (SetProperty(ref _selectedLieu, value) && value != null)
                {
                   // System.Diagnostics.Debug.WriteLine("Le lieu que je veux charger est : " + value.Title);
                    ChargerUnLieu(value);
                    SelectedLieu = null;
                }
            }
        }
        public string TitreMain
        {
            get => _titreMain;
            set => SetProperty(ref _titreMain, value);
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            getNom();
        }

        public MainPageViewModel(LoginResult loginResult)
        {
           // throw new NotImplementedException();
           _loginResult = loginResult;
           getNom();
           getLieux();
           CreateLieuCommand = new Command(CreateNewLieu);
           ChangeUserCommand = new Command(DetailUser);
          // LoadOnePlaceCommand = new Command<PlaceItemSummary>(ChargerUnLieu);

           //System.Diagnostics.Debug.WriteLine("Le token est : " + _loginResult.AccessToken);

        }

       /* private void ChargerUnLieu(PlaceItemSummary lieu)
        {
           // throw new NotImplementedException();
           
           System.Diagnostics.Debug.WriteLine("Le lieu que je veux charger est : " + lieu.Id);
        }*/

        public UserItem UserItem
        {
            get => _userItem;
            set => _userItem = value;
        }

        public async void getNom()
        {
            HttpResponseMessage reponse = await _api.Execute(HttpMethod.Get, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["UrlGetMe"], null, _loginResult.AccessToken);
            Response<UserItem> resp = new Response<UserItem>();

        
           // System.Diagnostics.Debug.WriteLine("URL : " + (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["UrlGetMe"]);
            
            if (reponse.IsSuccessStatusCode) {
                resp = await _api.ReadFromResponse<Response<UserItem>>(reponse);
                UserItem = resp.Data;
                TitreMain = "Bienvenue " + _userItem.FirstName + " " +  _userItem.LastName;
            }

            else
            {
                TitreMain =  resp.ErrorMessage;
            }
            
            
        }

        public async void getLieux()
        {
            HttpResponseMessage reponse = await _api.Execute(HttpMethod.Get, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["Places"], null, _loginResult.AccessToken);
            Response<ObservableCollection<PlaceItemSummary>> resp = new Response<ObservableCollection<PlaceItemSummary>>();

            if (reponse.IsSuccessStatusCode) {
                resp = await _api.ReadFromResponse<Response<ObservableCollection<PlaceItemSummary>>>(reponse);
                System.Diagnostics.Debug.WriteLine("PROUTTTTTT : " + resp.IsSuccess);

                ListeLieux = resp.Data;

                foreach (var lieu in ListeLieux)
                {
                    
                    //System.Diagnostics.Debug.WriteLine("Id Image : " + lieu.ImageId);
                   // HttpResponseMessage reponse = await _api.Execute(HttpMethod.Get, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["Places"], null, _loginResult.AccessToken);
                   // Response<ObservableCollection<PlaceItemSummary>> resp = new Response<ObservableCollection<PlaceItemSummary>>();
                   // resp = await _api.ReadFromResponse<Response<ObservableCollection<PlaceItemSummary>>>(reponse);

                   // lieu. = resp.Data;

                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("VIAIN PROUT : " + resp.ErrorMessage);
            }
        }

        public async void ChargerUnLieu(PlaceItemSummary lieuChoisi)
        {
           // System.Diagnostics.Debug.WriteLine("Lieu choisi : " + lieuChoisi.Title);
            
            await NavigationService.PushAsync(new LieuDetailPage(lieuChoisi, _loginResult.AccessToken));

         }
        
        public async void CreateNewLieu()
        {
            await NavigationService.PushAsync(new AjoutPhotoPage());

            // fonctions de création de lieu
        }
        
        public async void DetailUser()
        {
            // fonctions de création de lieu
            //System.Diagnostics.Debug.WriteLine("N KETIA");
            await NavigationService.PushAsync(new UserDetailPage(_userItem, _loginResult.AccessToken));
        }
        
        
    }
}
