using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeWifi.Services.Helpers;

namespace WelcomeWifi.Tests.Unit
{
    [TestFixture]
    public class StringScrubberTests
    {
        [Test]
        public void ScrubWifiFormatTest()
        {
            //make a string full of offenders
            const string s = "\\ ; , \" :";

            //maske sure everything is returned escaped
            const string expected = "\\\\ \\; \\, \\\" \\:";
            var scrubbed = s.ScrubWifiFormat();


            Assert.AreEqual(scrubbed, expected);
        }
    }
}
