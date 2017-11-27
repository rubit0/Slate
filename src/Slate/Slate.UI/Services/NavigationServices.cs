using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Newtonsoft.Json;

namespace Slate.UI.Services
{
    public class NavigationServices
    {
        public static async Task<PointOfInterestResult> GetPlaceMetaData(string placeName)
        {
            using (var client = new HttpClient())
            {
                var address = "http://api.duckduckgo.com/?q=" + placeName + "&format=json&pretty=1";
                var result = await client.GetAsync(address);
                if (!result.IsSuccessStatusCode)
                    return PointOfInterestResult.CreateFailed();

                var content = await result.Content.ReadAsStringAsync();
                var parsed = JsonConvert.DeserializeObject<dynamic>(content);
                var abstractSource = parsed.AbstractSource as string;
                var x = parsed["AbstractSource"] as string;
                if (!abstractSource.Contains("wikipedia", StringComparison.OrdinalIgnoreCase))
                    return PointOfInterestResult.CreateFailed();

                var abstractUrl = parsed.AbstractURL as string;

                return parsed;
            }
        }
    }

    public class PointOfInterestResult
    {
        public bool Success { get; set; }
        public string TownName { get; set; }
        public string Description { get; set; }
        public BitmapImage Thumbnail { get; set; }

        public static PointOfInterestResult CreateFailed()
        {
            return new PointOfInterestResult {Success = false};
        }
    }
}
