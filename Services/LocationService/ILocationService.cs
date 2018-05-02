using HtmlAgilityPack;
using System;

namespace Services
{
    public interface ILocationService
    {
        string CurrentLocation();
    }

    public class LocationService : ILocationService
    {
        private string _location;
        private readonly HtmlWeb _web = new HtmlWeb();
        private HtmlDocument _htmldoc;

        public LocationService()
        {
            _htmldoc = _web.Load(@"http://www.whatsmyip.org/");
            ExtractLocation();
        }

        public string CurrentLocation()
        {
            return _location;
        }

        private void ExtractLocation()
        {
            try
            {

                var node = _htmldoc.DocumentNode.SelectSingleNode("//*[@id=\"ip\"]");
                _location = node.InnerText;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
