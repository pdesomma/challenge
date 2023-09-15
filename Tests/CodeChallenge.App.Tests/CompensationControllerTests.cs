
using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CompensationCreate_Success_Test()
        {

            var comp =
                 new Compensation()
                 {
                     Id = "",
                     EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                     Salary = 500,
                     EffectiveDate = DateTime.Now.Date
                 };
            var content = new JsonSerialization().ToJson(comp);
            var req = _httpClient.PostAsync("api/compensation/", new StringContent(content, Encoding.UTF8, "application/json"));
            var response = req.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newComp = response.DeserializeContent<Compensation>();
            Assert.AreEqual(comp.EmployeeId, newComp.EmployeeId);
            Assert.AreEqual(comp.EffectiveDate, newComp.EffectiveDate); 
            Assert.AreEqual(comp.Salary, newComp.Salary);
        }

        [TestMethod]
        public void CompensationCreate_Fail_Test()
        {
            var comp =
                 new Compensation()
                 {
                     Id = "",
                     EmployeeId = "i am invalid",
                     Salary = 500,
                     EffectiveDate = DateTime.Now.Date
                 };
            var content = new JsonSerialization().ToJson(comp);
            var req = _httpClient.PostAsync("api/compensation/", new StringContent(content, Encoding.UTF8, "application/json"));
            var response = req.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
