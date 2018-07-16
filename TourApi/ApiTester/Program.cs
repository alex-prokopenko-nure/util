using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TourApi;
using TourApi.ViewModels;
using System.Threading;
using Microsoft.Extensions.Configuration;
using System.IO;
using TourApi.Models;
using TourApi.Helpers;

namespace ApiTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
