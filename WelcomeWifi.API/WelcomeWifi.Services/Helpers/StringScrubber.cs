using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeWifi.Services.Helpers
{
    public static class StringScrubber
    {
        public static string ScrubWifiFormat(this string s)
        {
            //https://github.com/zxing/zxing/wiki/Barcode-Contents#wifi-network-config-android
            //scrub special characters: Special characters \ ; , " and : should be escaped with a backslash (\) 

            return s.Replace("\\", "\\\\").Replace(";", "\\;").Replace(",", "\\,").Replace("\"", "\\\"").Replace(":", "\\:");
        }
    }
}
