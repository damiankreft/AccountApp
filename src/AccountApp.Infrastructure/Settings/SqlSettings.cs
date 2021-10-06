namespace AccountApp.Infrastructure.Settings
{
    public class SqlSettings
    {
        public string DbName { get; set; }
        public string ConnectionString { get; set; }
        public bool InMemory { get; set; }
    }
}