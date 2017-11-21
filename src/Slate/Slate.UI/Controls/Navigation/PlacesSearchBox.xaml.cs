using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Devices.SmartCards;
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
using Windows.UI.Xaml.Navigation;
using Slate.UI.Models.Navigation;

namespace Slate.UI.Controls.Navigation
{
    public sealed partial class PlacesSearchBox : UserControl
    {
        public MapControl Map { get; set; }
        public Geopoint CurrentPosition { get; set; }

        private readonly List<string> _failedMessageItemSource = new List<string> { "No Result found" };

        public PlacesSearchBox()
        {
            this.InitializeComponent();
        }

        private async void OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput)
                return;

            var result = await MapLocationFinder.FindLocationsAsync(sender.Text, CurrentPosition);
            if (result.Status == MapLocationFinderStatus.Success && result.Locations.Any())
                sender.ItemsSource = LocationSuggestion.CreateFromMapFinderResult(result);
            else
                sender.ItemsSource = _failedMessageItemSource;
        }

        private async void OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (sender.ItemsSource == _failedMessageItemSource)
            {
                sender.Text = string.Empty;
                return;
            }

            var suggestion = args.SelectedItem as LocationSuggestion;
            var route = await MapRouteFinder.GetDrivingRouteAsync(CurrentPosition,
                suggestion.Location.Point);

            switch (route.Status)
            {
                case MapRouteFinderStatus.Success:

                    break;
                case MapRouteFinderStatus.UnknownError:
                    break;
                case MapRouteFinderStatus.InvalidCredentials:
                    break;
                case MapRouteFinderStatus.NoRouteFound:
                    break;
                case MapRouteFinderStatus.NoRouteFoundWithGivenOptions:
                    break;
                case MapRouteFinderStatus.StartPointNotFound:
                    break;
                case MapRouteFinderStatus.EndPointNotFound:
                    break;
                case MapRouteFinderStatus.NoPedestrianRouteFound:
                    break;
                case MapRouteFinderStatus.NetworkFailure:
                    sender.ItemsSource = "No internet connection";
                    break;
                case MapRouteFinderStatus.NotSupported:
                    break;
            }
        }

        private async void OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (!(args.ChosenSuggestion is LocationSuggestion chosen))
            {
                sender.Text = string.Empty;
                return;
            }

            var scene = MapScene.CreateFromLocation(chosen.Location.Point);
            await Map.TrySetSceneAsync(scene);
        }
    }
}
