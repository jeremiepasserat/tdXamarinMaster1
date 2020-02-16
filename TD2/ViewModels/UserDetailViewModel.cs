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
    public class UserDetailViewModel : ViewModelBase
    {
        private UserItem _currentUser;
        private String _accessToken;
        private ApiClient api = new ApiClient();

        public ICommand ModifyUserCommand { get; }
        public ICommand ModifyPasswordCommand { get; }


        public UserItem CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public UserDetailViewModel(UserItem userItem, string accessToken)
        {
            ModifyUserCommand = new Command(changeUserInfos);
            ModifyPasswordCommand = new Command(changeUserMdp);

            _currentUser = userItem;
            _accessToken = accessToken;
            //throw new NotImplementedException();
        }

        public async void changeUserInfos()
        {
          UpdateProfileRequest user = new UpdateProfileRequest();
            user.FirstName = _currentUser.FirstName;
            user.LastName = _currentUser.LastName;

                    var method = new HttpMethod("PATCH");

            
                       HttpResponseMessage reponse = await api.Execute(method, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["UrlGetMe"], user, _accessToken);
                     //  UserItem updateResult = new UserItem();
                       Response<UserItem> resp = new Response<UserItem>();
                       
                       System.Diagnostics.Debug.WriteLine("Le statut de connexion est : " + reponse.StatusCode.ToString());
                       
                       if (reponse.IsSuccessStatusCode) {
                           //resp = await api.ReadFromResponse<Response<UserItem>>(reponse);
                           //updateResult = resp.Data;
                           
                           //System.Diagnostics.Debug.WriteLine("Le token de connexion est : " + loginResult.AccessToken);
                           await NavigationService.PopAsync();
                       }
           
                       else
                       {
                           System.Diagnostics.Debug.WriteLine("Problème dans la requête de connexion : " + resp.ErrorMessage);
                       }
        }
        
        public async void changeUserMdp()
        {
            await NavigationService.PushAsync(new ChangeMdp(_accessToken));
        }
    }
}