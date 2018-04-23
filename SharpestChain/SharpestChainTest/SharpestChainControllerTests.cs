namespace Com.Innoq.SharpestChain
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;

    using NUnit.Framework;

    [TestFixture]
    public class SharpestChainControllerTests
    {
        private readonly TestServer _server;

        private readonly HttpClient _client;

        public SharpestChainControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                                             .UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:5000");
        }

        [TestCase]
        public async Task NodeInfo()
        {
            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();
        }
    }
}
