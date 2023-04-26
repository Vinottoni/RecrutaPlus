namespace RecrutaPlus.Infra.Data
{
    public struct AppDataSettingsConfigutarionConst
    {
        //Settings
        public static string AppSettings => "appSettings";

        //DbProviderFactory
        public static string DBProviderFactoryType => "DBProviderFactoryType";
        public static string DBProviderFactoryName => "DBProviderFactoryName";
        public static string ConnectionStringDefault => "ConnectionStringDefault";

        //DbProviderFactory Integration
        public static string DbProviderFactoryTypeIntegration => "DbProviderFactoryTypeIntegration";
        public static string DbProviderFactoryNameIntegration => "DbProviderFactoryNameIntegration";
        public static string ConnectionStringDefaultIntegration => "ConnectionStringDefaultIntegration";

        public static string DbProviderFactoryTypeMsSQL => "MsSQL";
        public static string DbProviderFactoryTypeOracle => "Oracle";
        public static string DbProviderFactoryTypeMySQL => "MySQL";
    }
}