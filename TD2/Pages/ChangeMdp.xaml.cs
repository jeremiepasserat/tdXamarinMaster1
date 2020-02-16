﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TD2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeMdp : ContentPage
    {
        public ChangeMdp(string accessToken)
        {
            InitializeComponent();
            BindingContext = new ChangeMdpViewModel(accessToken);
        }
    }
}