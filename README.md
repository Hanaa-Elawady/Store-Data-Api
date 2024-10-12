Store-Api 
How to Configure the Application
If you're setting up the project locally, you will need to provide your own appsettings.json file in the root of the project. Here is a template for the file, which should be customized with your own values:

json
Copy code
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "your-connection-string-here",
    "IdentityConnection": "your-identity-connection-string-here",
    "Redis": "localhost"
  },
  "BaseUrl": "https://localhost:7108",
  "Token": {
    "Key": "your-token-key-here",
    "Issuer": "https://localhost:7108"
  },
  "Stripe": {
    "PublishableKey": "your-publishable-key-here",
    "SecretKey": "your-secret-key-here"
  }
}
Security Best Practices
Environment Variables: Consider storing sensitive information such as connection strings, API keys, and tokens in environment variables rather than hardcoding them in appsettings.json.

Git Ignore: Ensure that appsettings.json is included in your .gitignore file to prevent it from being accidentally committed in the future.

Production vs Development: Use separate configuration files for production (appsettings.Production.json) and development (appsettings.Development.json) to ensure that sensitive production settings are not used in development environments.
