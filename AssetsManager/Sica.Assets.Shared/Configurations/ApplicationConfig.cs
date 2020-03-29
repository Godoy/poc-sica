namespace Sica.Assets.Shared.Configurations
{
    public class ApplicationConfig
    {
        public ApplicationConfig()
        {
            Logging = new Logging();
            Database = new DatabaseConfig();
            Document = new DocumentConfig();
            Product = new ProductConfig();
            Insurer = new InsurerConfig();
            Authentication = new AuthenticationConfig();
        }

        public Logging Logging { get; set; }
        public DatabaseConfig Database { get; set; }
        public AuthenticationConfig Authentication { get; set; }
        public string[] CorsOrigins { get; set; }
        public ApiHosts apiHosts { get; set; }
        public DocumentConfig Document { get; set; }
        public ProductConfig Product { get; private set; }
        public InsurerConfig Insurer { get; set; }
    }

    public class InsurerConfig
    {
        public string SusepCode { get; set; }
        public string BranchOfficeCode { get; set; }
        public ProductConfig Product { get; set; }
    }

    public class ProductConfig
    {
        public string ProtocolNumber { get; set; } // TODO: find where this parameter should comes from
    }

    public class AuthenticationConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
    }

    public class Logging
    {
        public string TokenLogentries { get; set; }
    }

    public class DocumentConfig
    {
        public string DocumentBaseUrl { get; set; }
    }

    public class DatabaseConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AssemblyName { get; set; }
        public string DbFactoryName { get; set; }
    }

    public class ApiHosts
    {
        public string Sica { get; set; }
    }
}
