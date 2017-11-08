using Slate.UI.Pages;
using Slate.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Slate.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public NavigationViewModel ViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new NavigationViewModel { Name = "1234" };
        }

        private void Button_Navigation_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(NavigationPage));
        }

        private void Button_Media_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(MediaPage));
        }
    }
}
