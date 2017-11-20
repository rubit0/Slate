using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Slate.UI.Models.Navigation;
using Slate.UI.ViewModels;

namespace Slate.UI.Pages
{
    public sealed partial class NavigationPage : Page
    {
        public NavigationViewModel Model { get; set; }

        private readonly Geolocator _geolocator = new Geolocator();
        private bool _pageInitDone;

        public NavigationPage()
        {
            Model = new NavigationViewModel();
            NavigationCacheMode = NavigationCacheMode.Required;
            this.InitializeComponent();
        }

        private async Task SetupMap()
        {
            MapControlls.Opacity = 0;
            Map.Opacity = 0;
            Map.ZoomLevel = 16;
            Map.MapServiceToken =
                "UOBcq7qYTGSBYjpdwGvJ~ZoDecyJEsIhjGowIczjaTg~AmjcaSnBVS38715lpzn_KgdYiQf1AZ5KG-mLQHi0QWfPcl0xmo9WftqZ4fZtVUyO";
            Map.StyleSheet = MapStyleSheet.RoadDark();
            Map.TrafficFlowVisible = false;

            switch (await Geolocator.RequestAccessAsync())
            {
                case GeolocationAccessStatus.Unspecified:
                    break;
                case GeolocationAccessStatus.Allowed:
                    _geolocator.DesiredAccuracy = PositionAccuracy.High;
                    var position = await _geolocator.GetGeopositionAsync();
                    Model.CurrentPosition = position.Coordinate.Point;
                    Map.Center = Model.CurrentPosition;
                    FadeInAnimation.Begin();

                    _geolocator.PositionChanged += OnGeoPositionChanged;
                    break;
                case GeolocationAccessStatus.Denied:
                    break;
                default:
                    break;
            }
        }

        private async void OnGeoPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Model.CurrentPosition = args.Position.Coordinate.Point;
            });
        }

        private async void Page_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_pageInitDone)
                return;

            await SetupMap();
            _pageInitDone = true;
        }
    }
}
