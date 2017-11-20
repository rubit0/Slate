using System.Collections.Generic;
using System.Linq;
using Windows.Services.Maps;

namespace Slate.UI.Models.Navigation
{
    public class LocationSuggestion
    {
        public MapLocation Location { get; }

        public LocationSuggestion(MapLocation location)
        {
            Location = location;
        }

        public override string ToString()
        {
            return Location.DisplayName;
        }

        public static List<LocationSuggestion> CreateFromMapFinderResult(MapLocationFinderResult result)
        {
            return result.Locations.Select(l => new LocationSuggestion(l)).ToList();
        }
    }
}
