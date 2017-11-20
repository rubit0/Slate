using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

namespace Slate.UI.Controls.Navigation
{
    public sealed partial class BasicMapControls : UserControl
    {
        public MapControl Map { get; set; }
        public Geopoint CurrentPosition { get; set; }

        public BasicMapControls()
        {
            this.InitializeComponent();
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
            Map.DesiredPitch = 45;
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

        private void OnLocateMeClicked(object sender, RoutedEventArgs e)
        {
            Map.Center = CurrentPosition;
        }

        private void OnTrafficClicked(object sender, RoutedEventArgs e)
        {
            Map.TrafficFlowVisible = !Map.TrafficFlowVisible;
        }
    }
}
