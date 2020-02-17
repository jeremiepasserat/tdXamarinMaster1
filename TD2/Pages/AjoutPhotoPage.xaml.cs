using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using TD2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TD2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjoutPhotoPage : ContentPage
    {
        public AjoutPhotoPage(string loginResultAccessToken)
        {
            InitializeComponent();
            checkPermissions();
            BindingContext = new AjoutPhotoViewModel(loginResultAccessToken);
        }

        public async void checkPermissions()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
            
           // await DisplayAlert("Permission", "Permission photo : " + status, "Ok");
           
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                {
                   // await DisplayAlert("Need Photo Permission", "Gunna need that permission", "Ok");
                }

                status = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
            }

            if (status == PermissionStatus.Granted)
            {
                //await DisplayAlert("Photo Permission", "Okay", "Ok");

                //Query permission
            }
            else if (status != PermissionStatus.Unknown)
            {
                //location denied
            }

        }
}
}