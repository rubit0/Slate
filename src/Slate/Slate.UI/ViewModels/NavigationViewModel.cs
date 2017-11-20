using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls.Maps;

namespace Slate.UI.ViewModels
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MapLayer> Layers { get; }
        public MapElementsLayer PersonalLayer { get; }

        private Geopoint _geopoint;
        public Geopoint CurrentPosition
        {
            get => _geopoint;
            set
            {
                _geopoint = value;
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

            Layers = new ObservableCollection<MapLayer>(new[] {PersonalLayer});
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
