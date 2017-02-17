using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Threading;
using System.Globalization;
using TypeLite;
using TypeLite.Net4;
using System.Text.RegularExpressions;

namespace TAI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
