﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MusicPocketBook.BusinessLayer.ViewModels;
using MusicPocketBook.PresentationLayer.Views;

namespace MusicPocketBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BaseView app = new BaseView();
            BaseViewModel context = new BaseViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}