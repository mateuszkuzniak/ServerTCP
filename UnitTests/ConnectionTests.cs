using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLibrary;
//using Xunit;

namespace UnitTests
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void IPAddressTest()
        {
            //Assert.ThrowsException<FormatException>(() => new ServerTAP<LoginServerProtocol>(IPAddress.Parse("127."), 2048));
        }
        [TestMethod]
        public void PortTest()
        {
            //Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ServerTAP<LoginServerProtocol>(IPAddress.Parse("127.0.0.1"), 80).Port = 80);
        }
    }
}
