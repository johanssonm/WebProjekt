using System;

namespace Services
{
    public class Weather : IWeather
    {
        private int _temp;
        public double Temp => _temp;
        public int WeatherInfo { get; set; }

        public void SetTemp(double temp)
        {
            _temp = (int)Math.Round(temp);
        }
    }
}