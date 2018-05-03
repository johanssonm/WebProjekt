namespace Services
{
    public interface IWeatherService
    {
        IWeather Weather { get; }
        void CallService(double latitude, double longitude);
    }
}