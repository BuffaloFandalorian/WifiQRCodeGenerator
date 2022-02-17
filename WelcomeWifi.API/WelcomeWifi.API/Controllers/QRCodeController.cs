using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeWifi.Services.Dtos;
using WelcomeWifi.Services.Enums;
using WelcomeWifi.Services.Interfaces;

namespace WelcomeWifi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRCodeService _QRCodeService;
        public QRCodeController(IQRCodeService qrCodeService)
        {
            _QRCodeService = qrCodeService;
        }

        [HttpGet]
        public IActionResult GetQRCode(string message, int border)
        {
            return Content(_QRCodeService.GetAsSVG(message, border), "image/svg+xml; charset=utf-8");
        }

        [HttpGet("bmp")]
        public IActionResult GetQRCodeAsBMP(string message, int border, int size)
        {
            return File(_QRCodeService.GetAsBMP(message, border, size), "image/bmp");
        }

        [HttpGet("obj")]
        public IActionResult GetQRCodeAsObj(string message, int border, int size, int depth, int baseDepth, bool invert)
        {
            return File(_QRCodeService.GetAsObj(message, border, size, depth, baseDepth, invert), "text/plain", fileDownloadName: $"QRCode_{DateTime.Now}.obj");
        }

        [HttpPost("wifi")]
        public IActionResult GetWifiAsQRCode([FromBody] WifiDto wifiDto)
        {
            return Content(_QRCodeService.GetWifiAsSVG(wifiDto.SSID, wifiDto.Password, wifiDto.Hidden, wifiDto.Encryption, wifiDto.Border), "image/svg+xml; charset=utf-8");
        }
    }
}
