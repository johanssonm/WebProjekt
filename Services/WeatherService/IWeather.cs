namespace Services
{
    public interface IWeather
    {
        double Temp { get; }
        int WeatherInfo { get; set; }
        void SetTemp(double temp);
    }
}