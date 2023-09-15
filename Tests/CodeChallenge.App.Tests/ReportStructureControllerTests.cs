
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
    public class ReportStructureControllerTests
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
        public void GetDirectReports_Test()
        {
            var getReq = _httpClient.GetAsync("api/reports/direct/16a596ae-edd3-4847-99fe-c4518e82c86f");
            var response = getReq.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var report = response.DeserializeContent<ReportingStructure>();
            Assert.IsNotNull(report?.Employee);
            Assert.AreEqual(report.Employee.EmployeeId, "16a596ae-edd3-4847-99fe-c4518e82c86f");
            Assert.AreEqual(report.NumberOfReports, report.Employee.DirectReports.Count);
        }

        [TestMethod]
        public void GetFullReports_Test()
        {
            var getReq = _httpClient.GetAsync("api/reports/full/16a596ae-edd3-4847-99fe-c4518e82c86f");
            var response = getReq.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var report = response.DeserializeContent<ReportingStructure>();
            Assert.IsNotNull(report?.Employee);
            Assert.AreEqual(report.Employee.EmployeeId, "16a596ae-edd3-4847-99fe-c4518e82c86f");
            Assert.AreEqual(report.NumberOfReports, 4);
        }
    }
}
