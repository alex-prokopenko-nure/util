using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ApiTester.Tests
{
    abstract class ControllerTests
    {
        protected TestServer server;
        protected HttpClient client;

        public ControllerTests()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }
    }
}
