using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: HostingStartup(typeof(IdWedNu.Areas.Identity.IdentityHostingStartup))]

namespace IdWedNu.Areas.Identity.Pages.Account
{
    public class IdentityHostingStarups
    {
        public class IdentityHostingStartup : IHostingStartup
        {
            public void Configure(IWebHostBuilder builder)
            {
                builder.ConfigureServices((context, services) =>
                {
                });
            }
        }
    }
}
