using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace WebProjekt.UnitTests
{
    [TestClass]
    public class WeatherCodesTests
    {
        [TestMethod]
        public void CodesTest()
        {
            WeatherCodes.Init();

            var icon = WeatherCodes.Instance.GetWeatherIcon(800);

            Assert.AreEqual("http://openweathermap.org/img/w/01d.png", icon);
        }
    }
}