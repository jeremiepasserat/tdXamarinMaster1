using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using TD2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TD2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartePage : ContentPage
    {
       

        // Je casse le MVVM dans cette classe mais j'ai choisi de mettre ce code la à cause de la ligne "Content = stack;"
        public CartePage(PlaceItemSummary place)
        {
            InitializeComponent();
      
           Title = place.Title;

           Map map = new Map(
               MapSpan.FromCenterAndRadius(new Position(place.Latitude, place.Longitude), Distance.FromMiles(1)))
           {
               HeightRequest = 300,
               WidthRequest = 40,
               VerticalOptions = LayoutOptions.FillAndExpand
           };
                      
           var pin = new Pin () {
               Position = new Position(place.Latitude, place.Longitude),
               Label = place.Title
           };
           map.Pins.Add (pin);
           
           var stack = new StackLayout { Spacing = 0 };
           stack.Children.Add(map);
           Content = stack;
        }
    }
}