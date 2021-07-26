using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poke.Core.Translation;
using Poke.Core.Translation.Translators;
using Poke.Handlers;
using Poke.Services;

namespace Poke.Core.IoC
{
    public static class Registrations
    {
        /// <summary>
        /// Configure Inversion Of Control dependencies
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="configuration">Configuration provider</param>
        /// <param name="env">Hosting Environment</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment env)
        {
            services.Scan(x =>
            {
                x.AddTypes()
                    .FromAssemblyOf<PokemonInformationHandler>()
                    .AddClasses(c => c.Where(t => t.Name.EndsWith("Handler", StringComparison.OrdinalIgnoreCase)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();

                x.AddTypes()
                    .FromAssemblyOf<BasicInformationService>()
                    .AddClasses(c => c.Where(t => t.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });

            services.AddScoped<ITranslatorFactory, TranslatorFactory>();
            services.AddTransient<IValidTranslator, ShakespeareTranslator>();
            services.AddTransient<IValidTranslator, YodaTranslator>();
     

        }
    }
}
