using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using Common.Api.Dtos;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Storm.Mvvm;
using TD.Api.Dtos;
using TD2.Pages;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class AjoutPhotoViewModel : ViewModelBase
    {
        private string _accessToken;

        public ICommand GalleryCommand { get; }
        
        public ICommand CameraCommand { get; }
        
      //  public ICommand NewLieuCommand { get; }


      //  private string _nomPhotoUpload;

        private ImageSource _imageChoisie;

        //private bool _isImageLoad = false;

        public ImageSource ImageChoisie
        {
            get => _imageChoisie;
            set => SetProperty(ref _imageChoisie, value);
        }

        /*public string NomPhotoUpload
        {
            get => _nomPhotoUpload;
            set => SetProperty(ref _nomPhotoUpload, value);
        }*/


        public AjoutPhotoViewModel(string loginResultAccessToken)
        {
            //throw new System.NotImplementedException();
            _accessToken = loginResultAccessToken;
            GalleryCommand = new Command(chargerPhotoGallerie);
            CameraCommand = new Command(prendreNouvellePhoto);
           // NewLieuCommand = new Command(ecranInfosLieu);
        }
        
        // Marche pas (pour le moment)
        private async void prendreNouvellePhoto()
        {

            
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            
            if (photo != null)
            {
               // ImageChoisie = ImageSource.FromStream(() => photo.GetStream());
               // System.Diagnostics.Debug.WriteLine("Image Nom : " + _imageChoisie);
                loadImageIntoApi(ImageIntoByte(photo.GetStream()));
                

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Ca a planté la photo" + photo);

            }
        }

        private async void chargerPhotoGallerie()
        {
            
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                //System.Diagnostics.Debug.WriteLine("Je veux choisir une photo existante");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null){
                System.Diagnostics.Debug.WriteLine("photo photo : NON");
            return;
            }

            loadImageIntoApi(ImageIntoByte(file.GetStream()));

            /* ImageChoisie = ImageSource.FromStream(() => file.GetStream());
             System.Diagnostics.Debug.WriteLine("photo photo : " + _imageChoisie.AutomationId);
 
             loadImageIntoApi();
 */


            //throw new System.NotImplementedException();
        }
        
        public static byte[] ImageIntoByte(Stream input)
        {
            byte[] buffer = new byte[16*1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private async void loadImageIntoApi(byte[] image)
        {
           HttpClient client = new HttpClient();
           
           HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");
           request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
           
           MultipartFormDataContent requestContent = new MultipartFormDataContent();
           
           var imageContent = new ByteArrayContent(image);
           imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
           
           // Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
           requestContent.Add(imageContent, "file", "file.jpg");
           
           request.Content = requestContent;
           
           HttpResponseMessage response = await client.SendAsync(request);
           
           string result = await response.Content.ReadAsStringAsync();
           Response<ImageItem> resp = new Response<ImageItem>();
           
           
           
           if (response.IsSuccessStatusCode)
           {
             Console.WriteLine("Image uploaded! : " + result);
             System.Diagnostics.Debug.WriteLine("Image uploaded! : " + result);
             ApiClient _api = new ApiClient(); 
             resp = await _api.ReadFromResponse<Response<ImageItem>>(response);
             await NavigationService.PushAsync(new AjoutLieuPage(_accessToken, resp.Data.Id));

           }
           else
           {
               
               System.Diagnostics.Debug.WriteLine("Image pas uploaded! : " + response);

               
             //Debugger.Break();
           }
        }

        /*public byte[] ImageSourceToBytes(BitmapEncoder encoder, ImageSource imageSource)
        {
            byte[] bytes = null;
            var bitmapSource = imageSource as BitmapSource;

            if (bitmapSource != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }

            return bytes;
        } */

        
        

        
    }
}