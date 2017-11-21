using System;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;

namespace Slate.UI.Controls.Navigation
{
    public sealed partial class BasicMapControls : UserControl
    {
        private MapControl _map;
        public MapControl Map
        {
            get => _map;
            set
            {
                if (_map != null)
                    _map.HeadingChanged -= MapOnHeadingChanged;

                _map = value;
                _map.HeadingChanged += MapOnHeadingChanged;
            }
        }

        public Geopoint CurrentPosition { get; set; }

        private RotateTransform _compassRotation;

        public BasicMapControls()
        {
            this.InitializeComponent();
            _compassRotation = new RotateTransform();
        }

        private async void MapOnHeadingChanged(MapControl sender, object args)
        {
            _compassRotation.Angle = sender.Heading;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () => Compass.RenderTransform = _compassRotation);
        }

        private void OnToggleLight2DMode(object sender, RoutedEventArgs e)
        {
            Map.StyleSheet = MapStyleSheet.RoadLight();
        }

        private void OnToggleDark2DMode(object sender, RoutedEventArgs e)
        {
            Map.StyleSheet = MapStyleSheet.RoadDark();
        }

        private void OnToggle3DMode(object sender, RoutedEventArgs e)
        {
            if (!Map.Is3DSupported)
                return;

            Map.StyleSheet = MapStyleSheet.AerialWithOverlay();
        }

        private void OnPitchInClicked(object sender, RoutedEventArgs e)
        {
            Map.DesiredPitch = 65;
        }

        private void OnPitchOutClicked(object sender, RoutedEventArgs e)
        {
            Map.DesiredPitch = 0;
            Map.ZoomLevel = 18;
        }

        private void OnCenterOrientationClicked(object sender, RoutedEventArgs e)
        {
            Map.Heading = 0;
        }

        private async void OnLocateMeClicked(object sender, RoutedEventArgs e)
        {
            var scene = MapScene.CreateFromLocation(CurrentPosition);
            await Map.TrySetSceneAsync(scene);
        }

        private void OnTrafficClicked(object sender, RoutedEventArgs e)
        {
            Map.TrafficFlowVisible = !Map.TrafficFlowVisible;
        }
    }
}
