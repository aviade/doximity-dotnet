using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doximity.Api.Tests
{
    [TestClass]
    public class DoximityDataAccessTests
    {
        [TestMethod]
        public void Auth_RedirecSuccessfull()
        {
            var doximityDataAccess = new DoximityDataAccess();
            const string appId = "209be0bf467ec5188d5c1be32cc0a1d3fa3cfe40003fdde5ff9b0f54af98251b";
            const string redirectUri = "http://aviade-mobile/doximity/api/auth";
            doximityDataAccess.Authenticate(appId, new Uri(redirectUri));
        }
    }
}
