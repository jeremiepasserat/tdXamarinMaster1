﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TD2.ViewModels;

namespace TD2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InscriptionPage : ContentPage
    {
        public InscriptionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            BindingContext = new InscriptionViewModel();
        }
    }
}