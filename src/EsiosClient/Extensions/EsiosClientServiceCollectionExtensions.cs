using System;
using Microsoft.Extensions.DependencyInjection;

namespace EsiosClient;

public static class EsiosClientServiceCollectionExtensions
{
    public static IServiceCollection AddEsiosClient(
        this IServiceCollection services, 
        Action<EsiosClientOptions> configureOptions)
    {
        if (configureOptions == null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        services.Configure(configureOptions);

        services.AddHttpClient<IEsiosClient, EsiosClient>((provider, client) =>
        {
            var options = provider.GetRequiredService<Microsoft.Extensions.Options.IOptions<EsiosClientOptions>>().Value;
            
            if (!string.IsNullOrWhiteSpace(options.BaseAddress))
            {
                client.BaseAddress = new Uri(options.BaseAddress);
            }
            
            if (!string.IsNullOrWhiteSpace(options.PersonalToken))
            {
                // Both standard Authorization header and custom x-api-key could be needed,
                // depending on the API's actual requirement. We'll set Authorization header which is commonly used.
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", options.PersonalToken);
                
                // Some ESIOS integrations also look for x-api-key:
                client.DefaultRequestHeaders.Add("x-api-key", options.PersonalToken);
            }
            
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        });

        return services;
    }
}
