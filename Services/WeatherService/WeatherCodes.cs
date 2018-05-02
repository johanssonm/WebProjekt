using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class WeatherCodes
    {
        public static WeatherCodes Instance { get; private set; }
        private Dictionary<int, string> weatherCodes;

        public string GetWeatherIcon(int code)
        {
            var icon = weatherCodes.Where(c => c.Key == code).Select(i => i.Value).FirstOrDefault();

            return icon;
        }

        private WeatherCodes()
        {
            weatherCodes = new Dictionary<int, string>
            {
                {200, "http://openweathermap.org/img/w/11d.png"},
                {201, "http://openweathermap.org/img/w/11d.png"},
                {202, "http://openweathermap.org/img/w/11d.png"},
                {210, "http://openweathermap.org/img/w/11d.png" },
                {211, "http://openweathermap.org/img/w/11d.png" },
                {212, "http://openweathermap.org/img/w/11d.png" },
                {221, "http://openweathermap.org/img/w/11d.png" },
                {230, "http://openweathermap.org/img/w/11d.png" },
                {231, "http://openweathermap.org/img/w/11d.png" },
                {232, "http://openweathermap.org/img/w/11d.png" },
                //
                {300, "http://openweathermap.org/img/w/09d.png" },
                {301, "http://openweathermap.org/img/w/09d.png" },
                {302, "http://openweathermap.org/img/w/09d.png" },
                {310, "http://openweathermap.org/img/w/09d.png" },
                {311, "http://openweathermap.org/img/w/09d.png" },
                {312, "http://openweathermap.org/img/w/09d.png" },
                {313, "http://openweathermap.org/img/w/09d.png" },
                {314, "http://openweathermap.org/img/w/09d.png" },
                {321, "http://openweathermap.org/img/w/09d.png" },
                //
                {500, "http://openweathermap.org/img/w/10d.png" },
                {501, "http://openweathermap.org/img/w/10d.png" },
                {502, "http://openweathermap.org/img/w/10d.png" },
                {503, "http://openweathermap.org/img/w/10d.png" },
                {504, "http://openweathermap.org/img/w/10d.png" },
                {511, "http://openweathermap.org/img/w/13d.png" },
                {520, "http://openweathermap.org/img/w/09d.png" },
                {521, "http://openweathermap.org/img/w/09d.png" },
                {522, "http://openweathermap.org/img/w/09d.png" },
                {531, "http://openweathermap.org/img/w/09d.png" },
                //
                {600, "http://openweathermap.org/img/w/13d.png" },
                {601, "http://openweathermap.org/img/w/13d.png" },
                {602, "http://openweathermap.org/img/w/13d.png" },
                {611, "http://openweathermap.org/img/w/13d.png" },
                {612, "http://openweathermap.org/img/w/13d.png" },
                {615, "http://openweathermap.org/img/w/13d.png" },
                {616, "http://openweathermap.org/img/w/13d.png" },
                {620, "http://openweathermap.org/img/w/13d.png" },
                {621, "http://openweathermap.org/img/w/13d.png" },
                {622, "http://openweathermap.org/img/w/13d.png" },
                //
                {701, "http://openweathermap.org/img/w/50d.png" },
                {711, "http://openweathermap.org/img/w/50d.png" },
                {721, "http://openweathermap.org/img/w/50d.png" },
                {731, "http://openweathermap.org/img/w/50d.png" },
                {741, "http://openweathermap.org/img/w/50d.png" },
                {751, "http://openweathermap.org/img/w/50d.png" },
                {761, "http://openweathermap.org/img/w/50d.png" },
                {762, "http://openweathermap.org/img/w/50d.png" },
                {771, "http://openweathermap.org/img/w/50d.png" },
                {781, "http://openweathermap.org/img/w/50d.png" },
                //
                {800, "http://openweathermap.org/img/w/01d.png" },
                //
                {801, "http://openweathermap.org/img/w/02d.png" },
                {802, "http://openweathermap.org/img/w/03d.png" },
                {803, "http://openweathermap.org/img/w/04d.png" },
                {804, "http://openweathermap.org/img/w/04d.png" }
            };
        }

        public static void Init()
        {
            Instance = new WeatherCodes();
        }
    }
}