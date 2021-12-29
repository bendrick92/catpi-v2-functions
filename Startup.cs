using System;
using Azure.Identity;
using CatPiV2.Functions;
using CatPiV2.Functions.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace CatPiV2.Functions
{
  public class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddOptions<AppSettings>()
        .Configure<IConfiguration>((settings, configuration) => {
          configuration.GetSection("Values").Bind(settings);
        });      
    }

    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
      var config = builder.ConfigurationBuilder.Build();
      var keyVaultUri = config["Azure:KeyVault:Uri"];
      
      if (!string.IsNullOrEmpty(keyVaultUri))
      {
        builder.ConfigurationBuilder
          .SetBasePath(Environment.CurrentDirectory)
          .AddAzureKeyVault(new Uri(config["Azure:KeyVault:Uri"]), new DefaultAzureCredential())
          .AddJsonFile("local.settings.json", true)
          .AddEnvironmentVariables()
          .Build();
      }
      else
      {
        // No Key Vault
        builder.ConfigurationBuilder
          .SetBasePath(Environment.CurrentDirectory)
          .AddJsonFile("local.settings.json", true)
          .AddEnvironmentVariables()
          .Build();
      }
    }
  }
}