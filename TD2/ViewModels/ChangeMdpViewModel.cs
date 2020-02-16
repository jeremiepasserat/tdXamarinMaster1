using System;
using System.Net.Http;
using System.Windows.Input;
using Common.Api.Dtos;
using Newtonsoft.Json;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;
namespace TD2.ViewModels
{
    public class ChangeMdpViewModel : ViewModelBase
    {

        private String _oldMdp;
        private String _newMdp;
        private String _accessToken;
        private ApiClient api = new ApiClient();

        public ICommand GoToChangeCommand { get; }

        public string NewMdp
        {
            get => _newMdp;
            set => SetProperty(ref _newMdp, value);
        }
        
        public string OldMdp
        {
            get => _oldMdp;
            set => SetProperty(ref _oldMdp, value);
        }

        public ChangeMdpViewModel(string accessToken)
        {
            _accessToken = accessToken;
            GoToChangeCommand = new Command(ChangePasswd);
        }

        public async void ChangePasswd()
        {
            
            UpdatePasswordRequest password = new UpdatePasswordRequest();
            password.OldPassword = _oldMdp;
            password.NewPassword = _newMdp;

            var method = new HttpMethod("PATCH");

            
            HttpResponseMessage reponse = await api.Execute(method, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["Urlpassword"], password, _accessToken);
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
        
        
    }
}