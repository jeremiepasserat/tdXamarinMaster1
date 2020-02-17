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
        public LieuDetailPage(PlaceItemSummary lieuChoisi, string loginResultAccessToken)
        {
            InitializeComponent();
            Title = lieuChoisi.Title;
            BindingContext = new LieuDetailViewModel(lieuChoisi, loginResultAccessToken);
        }
    }
}