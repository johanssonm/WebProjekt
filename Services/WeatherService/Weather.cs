using System;

namespace Services
{
    public class Weather : IWeather
    {
        private const double KelvinToCelcius = 273.15;
        private int _temp;
        public double Temp => _temp;
        public int WeatherInfo { get; set; }

        public void SetTemp(double tempKelvin)
        {
            _temp = (int)Math.Round(tempKelvin - KelvinToCelcius);
        }
    }
}