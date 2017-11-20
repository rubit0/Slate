using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Slate.UI.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(NavigationPage));
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
