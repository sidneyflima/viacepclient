using Microsoft.Extensions.DependencyInjection;
using System;
using ViaCepClient.Client;
using ViaCepClient.Http;

namespace ViaCepClient.Extensions.DependencyInjection
{
    /// <summary>
    /// Add ViaCepClient to dependency injection
    /// </summary>
    public static class ViaCepDependencyInjection
    {
        /// <summary>
        /// Add IViaCepClient to dependency injection. Though this method, it is possible to configure ViaCepClientOptions instance
        /// </summary>
        public static IServiceCollection AddViaCepClient(this IServiceCollection services, Action<ViaCepClientOptions> configure)
        {
            services.AddHttpClient<IRestClient, RestClient>();
            services.AddSingleton<ViaCepClientOptions>(p =>
            {
                ViaCepClientOptions options = new ViaCepClientOptions();
                configure?.Invoke(options);

                return options;
            });
            services.AddSingleton<IViaCepRequestBuilder, ViaCepRequestBuilder>();
            services.AddScoped<IViaCepClient, ViaCepClient.Client.ViaCepClient>();

            return services;
        }
    }
}
