using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using TD2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TD2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LieuDetailPage : ContentPage
    {

        private String _token;
        private PlaceItemSummary _placeItem;
        
        public LieuDetailPage(PlaceItemSummary lieuChoisi, string loginResultAccessToken)
        {
            InitializeComponent();
            _token = loginResultAccessToken;
            Title = lieuChoisi.Title;
            _placeItem = lieuChoisi;
            BindingContext = new LieuDetailViewModel(lieuChoisi, loginResultAccessToken);
        }

        // Permet la maj d'affichage en cas d'ajout de commentaire
        protected override void OnAppearing()
        {
            
            Title = _placeItem.Title;
            BindingContext = new LieuDetailViewModel(_placeItem, _token);
            base.OnAppearing();
        }
    }
}