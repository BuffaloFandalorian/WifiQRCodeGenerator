using Net.Codecrete.QrCodeGenerator;
using WelcomeWifi.Services.Helpers;
using WelcomeWifi.Services.Interfaces;
using Svg;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing;
using WelcomeWifi.Obj;

namespace WelcomeWifi.Services.Services
{
    public class QRCodeService : IQRCodeService
    {
        public string GetAsSVG(string message, int border)
        {
            var qr = QrCode.EncodeText(message, QrCode.Ecc.Medium);
            return qr.ToSvgString(border);
        }

        private XmlDocument GetAsXMLDocument(string message, int border)
        {
            var qr = QrCode.EncodeText(message, QrCode.Ecc.Medium);
            var doc = new XmlDocument();
            doc.LoadXml(qr.ToSvgString(border));
            return doc;
        }

        private ObjDocument GetObjDocument(string message, int border, int size, int depth, int baseDepth, bool invert)
        {
            var qr = QrCode.EncodeText(message, QrCode.Ecc.Medium);

            ObjDocument obj = new ObjDocument();

            var startDepth = 0;
            var endDepth = depth;

            startDepth += baseDepth;
            endDepth += baseDepth;

            obj.AddCube(0, 0, size + (2 * border), 0, baseDepth);

            var pixelDepth = size / qr.Size;
            for (int i = 0; i < qr.Size * qr.Size; i++)
            {
                var x = i / qr.Size;
                var y = i % qr.Size;
                var stroke = qr.GetModule(x, y);

                stroke ^= invert;

                if (stroke)
                {

                    var adjustedX = (x * pixelDepth) + border;
                    var adjustedY = (y * pixelDepth) + border;

                    obj.AddCube(adjustedX, adjustedY, pixelDepth, startDepth, endDepth);
                }
            }

            return obj;
        }

        public string GetAsObjString(string message, int border, int size, int depth, int baseDepth, bool invert)
        {
            return GetObjDocument(message, border, size, depth, baseDepth, invert).GetAsString();
        }

        public byte [] GetAsObj(string message, int border, int size, int depth, int baseDepth, bool invert)
        {
            return GetObjDocument(message, border, size, depth, baseDepth, invert).GetAsByteArray();
        }

        public byte [] GetAsBMP(string message, int border, int size)
        {
            var qr = QrCode.EncodeText(message, QrCode.Ecc.Medium);
            Bitmap bitmap = new Bitmap(size + (border * 2), size + (border * 2));
            var pixelDepth = size / qr.Size;
            for(int i = 0; i < qr.Size * qr.Size; i++)
            {
                var x = i / qr.Size;
                var y = i % qr.Size;
                var strokeColor = qr.GetModule(x, y) ? Color.White : Color.Black;

                for(int j = 0; j < pixelDepth; j++)
                {
                    for(int k = 0; k < pixelDepth; k++)
                    {
                        var adjuestedX = (x * pixelDepth) + j;
                        var adjuestedY = (y * pixelDepth) + k;
                        bitmap.SetPixel(adjuestedX + border, adjuestedY + border, strokeColor);
                    }
                }
            }
            return bitmap.ToByteArray(ImageFormat.Bmp);
        }

        public string GetWifiAsSVG(string SSID, string password, bool hidden, string encryption, int border)
        {
            var s = GetWifiString(SSID, password, hidden, encryption);
            return GetAsSVG(s, border);
        }

        public string GetWifiString(string SSID, string password, bool hidden, string encryption)
        {
            var s = $"WIFI:S:{SSID.ScrubWifiFormat()};P:{password.ScrubWifiFormat()};H:{hidden.ToString().ToLower()};";
            if (!String.IsNullOrEmpty(encryption))
            {
                s += $"T:{encryption.ScrubWifiFormat()};";
            }
            s += ";";
            return s;
        }
    }
}
