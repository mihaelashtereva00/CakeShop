namespace CakeShop.Models.Models.Configurations
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProcessedCollection { get; set; }
        public string PurcahsesCollection { get; set; }
    }
}
