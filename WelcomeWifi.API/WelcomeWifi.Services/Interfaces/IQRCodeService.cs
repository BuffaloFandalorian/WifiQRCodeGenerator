using Net.Codecrete.QrCodeGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeWifi.Services.Enums;

namespace WelcomeWifi.Services.Interfaces
{
    public interface IQRCodeService
    {
        byte [] GetAsBMP(string message, int border, int size);
        byte[] GetAsObj(string message, int border, int size, int depth, int baseDepth, bool invert);
        string GetAsObjString(string message, int border, int size, int depth, int baseDepth, bool invert);
        string GetAsSVG(string message, int border);
        string GetWifiAsSVG(string sSID, string password, bool hidden, string encryption, int border);
        string GetWifiString(string SSID, string password, bool hidden, string encryption);
    }
}
