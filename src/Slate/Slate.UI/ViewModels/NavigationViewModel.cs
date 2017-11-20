using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls.Maps;

namespace Slate.UI.ViewModels
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MapLayer> Layers { get; }
        public MapElementsLayer PersonalLayer { get; }
        public MapIcon CurrentPositionIcon { get; }

        private Geopoint _geopoint;
        public Geopoint CurrentPosition
        {
            get => _geopoint;
            set
            {
                _geopoint = value;
                CurrentPositionIcon.Location = value;
                OnPropertyChanged();
            }
        }

        public NavigationViewModel()
        {
            PersonalLayer = new MapElementsLayer
            {
                ZIndex = 0,
                Visible = true,
                Dispatcher = { CurrentPriority = CoreDispatcherPriority.Normal}
            };

            CurrentPositionIcon = new MapIcon
            {
                Tag = "Current",
                Location = CurrentPosition,
                NormalizedAnchorPoint = new Point(0.5, 0.5),
                ZIndex = 0
            };

            PersonalLayer.MapElements.Add(CurrentPositionIcon);

            Layers = new ObservableCollection<MapLayer>(new[] {PersonalLayer});
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
