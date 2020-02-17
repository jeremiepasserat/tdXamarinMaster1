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
    public partial class AjoutLieuPage : ContentPage
    {
        public AjoutLieuPage()
        {
            InitializeComponent();
            BindingContext = new AjoutLieuViewModel();
        }
    }
}