using System;
using System.IO;
using System.Windows.Input;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Storm.Mvvm;
using Xamarin.Forms;

namespace TD2.ViewModels
{
    public class AjoutPhotoViewModel : ViewModelBase
    {
        private string _accessToken;

        public ICommand GalleryCommand { get; }
        
        public ICommand CameraCommand { get; }
        
        public ICommand NewLieuCommand { get; }


        private string _nomPhotoUpload;

        public string NomPhotoUpload
        {
            get => _nomPhotoUpload;
            set => SetProperty(ref _nomPhotoUpload, value);
        }


        public AjoutPhotoViewModel(string loginResultAccessToken)
        {
            //throw new System.NotImplementedException();
            _accessToken = loginResultAccessToken;
            GalleryCommand = new Command(chargerPhotoGallerie);
            CameraCommand = new Command(prendreNouvellePhoto);
            NewLieuCommand = new Command(ecranInfosLieu);
        }

        private void ecranInfosLieu()
        {
            // Il faut tester qu'une photo aie bien été choisie pour le nouveau lieu
            
            // On envoie l'id de cette photo sur la page suivante
            
        }

        private async void prendreNouvellePhoto()
        {

            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            
            System.Diagnostics.Debug.WriteLine("Image Nom : " + photo);

            if (photo != null)
            {
                ImageSource image = ImageSource.FromStream(() => photo.GetStream());
                

            }
        }

        private void chargerPhotoGallerie()
        {
            System.Diagnostics.Debug.WriteLine("Je veux choisir une photo existante");
            //throw new System.NotImplementedException();
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