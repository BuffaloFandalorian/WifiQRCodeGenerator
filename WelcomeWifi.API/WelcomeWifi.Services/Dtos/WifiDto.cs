using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeWifi.Services.Dtos
{
    public class WifiDto
    {
        public string SSID { get; set; }
        public string Password { get; set; }
        public string Encryption { get; set; }
        public bool Hidden { get; set; }
        public int Border { get; set; }
    }
}
