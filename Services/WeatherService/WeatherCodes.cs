using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class WeatherCodes
    {
        public static WeatherCodes Instance { get; private set; }
        private readonly Dictionary<int, string> _weatherCodes;
        private readonly string _imgLink = "http://openweathermap.org/img/w/";
        private readonly string _pngSuffix = ".png";

        public string GetWeatherIcon(int code)
        {
            return _weatherCodes.Where(c => c.Key == code).Select(i => i.Value).FirstOrDefault();
        }

        private WeatherCodes()
        {
            _weatherCodes = new Dictionary<int, string>
            {
                {200, $"{_imgLink}11d{_pngSuffix}"},
                {201, $"{_imgLink}11d{_pngSuffix}"},
                {202, $"{_imgLink}11d{_pngSuffix}"},
                {210, $"{_imgLink}11d{_pngSuffix}" },
                {211, $"{_imgLink}11d{_pngSuffix}" },
                {212, $"{_imgLink}11d{_pngSuffix}" },
                {221, $"{_imgLink}11d{_pngSuffix}" },
                {230, $"{_imgLink}11d{_pngSuffix}" },
                {231, $"{_imgLink}11d{_pngSuffix}" },
                {232, $"{_imgLink}11d{_pngSuffix}" },
                //
                {300, $"{_imgLink}09d{_pngSuffix}" },
                {301, $"{_imgLink}09d{_pngSuffix}" },
                {302, $"{_imgLink}09d{_pngSuffix}" },
                {310, $"{_imgLink}09d{_pngSuffix}" },
                {311, $"{_imgLink}09d{_pngSuffix}" },
                {312, $"{_imgLink}09d{_pngSuffix}" },
                {313, $"{_imgLink}09d{_pngSuffix}" },
                {314, $"{_imgLink}09d{_pngSuffix}" },
                {321, $"{_imgLink}09d{_pngSuffix}" },
                //
                {500, $"{_imgLink}10d{_pngSuffix}" },
                {501, $"{_imgLink}10d{_pngSuffix}" },
                {502, $"{_imgLink}10d{_pngSuffix}" },
                {503, $"{_imgLink}10d{_pngSuffix}" },
                {504, $"{_imgLink}10d{_pngSuffix}" },
                {511, $"{_imgLink}13d{_pngSuffix}" },
                {520, $"{_imgLink}09d{_pngSuffix}" },
                {521, $"{_imgLink}09d{_pngSuffix}" },
                {522, $"{_imgLink}09d{_pngSuffix}" },
                {531, $"{_imgLink}09d{_pngSuffix}" },
                //
                {600, $"{_imgLink}13d{_pngSuffix}" },
                {601, $"{_imgLink}13d{_pngSuffix}" },
                {602, $"{_imgLink}13d{_pngSuffix}" },
                {611, $"{_imgLink}13d{_pngSuffix}" },
                {612, $"{_imgLink}13d{_pngSuffix}" },
                {615, $"{_imgLink}13d{_pngSuffix}" },
                {616, $"{_imgLink}13d{_pngSuffix}" },
                {620, $"{_imgLink}13d{_pngSuffix}" },
                {621, $"{_imgLink}13d{_pngSuffix}" },
                {622, $"{_imgLink}13d{_pngSuffix}" },
                //
                {701, $"{_imgLink}50d{_pngSuffix}" },
                {711, $"{_imgLink}50d{_pngSuffix}" },
                {721, $"{_imgLink}50d{_pngSuffix}" },
                {731, $"{_imgLink}50d{_pngSuffix}" },
                {741, $"{_imgLink}50d{_pngSuffix}" },
                {751, $"{_imgLink}50d{_pngSuffix}" },
                {761, $"{_imgLink}50d{_pngSuffix}" },
                {762, $"{_imgLink}50d{_pngSuffix}" },
                {771, $"{_imgLink}50d{_pngSuffix}" },
                {781, $"{_imgLink}50d{_pngSuffix}" },
                //
                {800, $"{_imgLink}01d{_pngSuffix}" },
                //
                {801, $"{_imgLink}02d{_pngSuffix}" },
                {802, $"{_imgLink}03d{_pngSuffix}" },
                {803, $"{_imgLink}04d{_pngSuffix}" },
                {804, $"{_imgLink}04d{_pngSuffix}" }
            };
        }

        public static void Init()
        {
            Instance = new WeatherCodes();
        }
    }
}