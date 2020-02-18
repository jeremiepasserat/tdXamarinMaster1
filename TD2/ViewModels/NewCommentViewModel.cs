using System;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Common.Api.Dtos;
using Newtonsoft.Json;
using Storm.Mvvm;
using TD.Api.Dtos;
using TD2.Pages;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class NewCommentViewModel  : ViewModelBase
    { 
        private  readonly HttpClient _client = new HttpClient();

        private string _accessToken;
        private int _idPlace;

        public ICommand PublishCommand { get; }

        private string _commentText;

        //private UserItem _user;

        public string CommentText
        {
            get => _commentText;
            set => SetProperty(ref _commentText, value);
        }

        public NewCommentViewModel(string accessToken, int placeId)
        {
            _accessToken = accessToken;
            PublishCommand = new Command(publierCommentaire);
            _idPlace = placeId;
        }

        private async void publierCommentaire()
        {

            System.Diagnostics.Debug.WriteLine("PROUT");
            
            CreateCommentRequest commentRequest = new CreateCommentRequest();
            commentRequest.Text = _commentText;
            
            // On passe en "manuel" sans l'aide de la classe ApiExecute à cause du media type différent
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, (string)Application.Current.Resources["Url"] + (string)Application.Current.Resources["Places"] + 
                                                                                 "/" + _idPlace + (string)Application.Current.Resources["Comments"]);
            request.Content = new StringContent(JsonConvert.SerializeObject(commentRequest), Encoding.UTF8, "application/json-patch+json");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");
            
            await _client.SendAsync(request);
            
            await NavigationService.PopAsync();

            
        }
    }
}