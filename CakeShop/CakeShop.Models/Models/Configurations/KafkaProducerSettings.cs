namespace CakeShop.Models.Models.Configurations
{
    public class KafkaProducerSettings
    {
        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
    }
}
