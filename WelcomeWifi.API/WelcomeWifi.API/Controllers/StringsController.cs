using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeWifi.Services.Dtos;
using WelcomeWifi.Services.Interfaces;

namespace WelcomeWifi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringsController : ControllerBase
    {
        private readonly IQRCodeService _QRCodeService;
        public StringsController(IQRCodeService qrCodeService)
        {
            _QRCodeService = qrCodeService;
        }

        [HttpPost("wifi")]
        public IActionResult GetWifiAsQRCode([FromBody] WifiDto wifiDto)
        {
            return Ok(_QRCodeService.GetWifiString(wifiDto.SSID, wifiDto.Password, wifiDto.Hidden, wifiDto.Encryption));
        }

    }
}
