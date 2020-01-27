﻿using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Serilog.Extensions.Logging
{
    public static class Extensions
    {
        public static IWebAssemblyHostBuilder UseSerilog(this IWebAssemblyHostBuilder builder)
        {
            builder.ConfigureServices((context, serviceCollection) =>
            {
                serviceCollection.AddSingleton(typeof(ILoggerFactory), new SerilogLoggerFactory());

                foreach (var item in serviceCollection.Where(x => x.ServiceType == typeof(ILogger<>)).ToArray())
                {
                    serviceCollection.Remove(item);
                }

                serviceCollection.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddProvider(new SerilogLoggerProvider());
                });
            });

            return builder;
        }
    }
}
