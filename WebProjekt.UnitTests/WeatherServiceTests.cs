using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace WebProjekt.UnitTests
{
    [TestClass]
    public class WeatherServiceTests
    {
        [TestMethod]
        public void OpenWeatherMapServiceTest()
        {
            var service = new OpenWeatherMapService();
            service.CallService(57.708944599999995, 11.966970799999999);
        }
    }
}
