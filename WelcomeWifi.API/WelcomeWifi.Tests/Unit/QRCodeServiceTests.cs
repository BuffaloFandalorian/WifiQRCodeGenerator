using NUnit.Framework;
using WelcomeWifi.Services.Interfaces;
using WelcomeWifi.Services.Services;
using AutoFixture;
using WelcomeWifi.Services.Dtos;
using WelcomeWifi.Services.Helpers;

namespace WelcomeWifi.Tests.Unit
{
    [TestFixture]
    public class QRCodeServiceTests
    {
        private IQRCodeService _QRCodeService;
        private IFixture _fixture;
        [SetUp]
        public void SetUp()
        {
            _QRCodeService = new QRCodeService();
            _fixture = new Fixture();
        }

        [Test]
        public void WifiStringFormatTests()
        {
            var mockDto = _fixture.Create<WifiDto>();

            var s = _QRCodeService.GetWifiString(mockDto.SSID, mockDto.Password, mockDto.Hidden, mockDto.Encryption);

            Assert.IsTrue(s.Contains("WIFI:"));
            Assert.IsTrue(s.Contains($"P:{mockDto.Password.ScrubWifiFormat()};"));
            Assert.IsTrue(s.Contains($"S:{mockDto.SSID.ScrubWifiFormat()};"));
            Assert.IsTrue(s.Contains($"H:{mockDto.Hidden.ToString().ToLower()};"));
            Assert.IsTrue(s.Contains($"T:{mockDto.Encryption.ScrubWifiFormat()};"));
        }
    }
}
