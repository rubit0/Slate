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
        private MapIcon _currentPositionNeedle;

        public NavigationPage()
        {
            Model = new NavigationViewModel();
            this.InitializeComponent();

        }

        private async Task SetupMap()
        {
            MapControlls.Opacity = 0;
            Map.Opacity = 0;

            Map.MapServiceToken =
                "UOBcq7qYTGSBYjpdwGvJ~ZoDecyJEsIhjGowIczjaTg~AmjcaSnBVS38715lpzn_KgdYiQf1AZ5KG-mLQHi0QWfPcl0xmo9WftqZ4fZtVUyO";
            Map.StyleSheet = MapStyleSheet.RoadDark();
            Map.TrafficFlowVisible = false;
            NavigationCacheMode = NavigationCacheMode.Required;

            var accessTatus = await Geolocator.RequestAccessAsync();
            Map.ZoomLevel = 16;

            switch (accessTatus)
            {
                case GeolocationAccessStatus.Unspecified:
                    break;
                case GeolocationAccessStatus.Allowed:
                    _geolocator.DesiredAccuracy = PositionAccuracy.High;
                    var position = await _geolocator.GetGeopositionAsync();
                    Map.Center = position.Coordinate.Point;

                    _currentPositionNeedle = new MapIcon
                    {
                        Location = position.Coordinate.Point,
                        NormalizedAnchorPoint = new Point(0.5, 1.0),
                        ZIndex = 0
                    };

                    Model.PersonalLayer.MapElements.Add(_currentPositionNeedle);
                    PitchAnimation.Begin();

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
                _currentPositionNeedle.Location = args.Position.Coordinate.Point;
            });
        }

        private async void Page_OnLoaded(object sender, RoutedEventArgs e)
        {
            await SetupMap();
        }
    }
}
