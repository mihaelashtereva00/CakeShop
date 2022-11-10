using CakeShop.DL.Interfaces;
using CakeShop.MessagePack;
using CakeShop.Models.Models.Configurations;
using CakeShop.Models.ModelsMongoDB;
using Confluent.Kafka;
using DnsClient.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CakeShop.BL.Kafka
{
    public class PurcahseProducerService : IHostedService
    {
        private IProducer<Guid, Purchase> _producer;
        private ProducerConfig _config;
        private IPurchaseRepository _purchaseRepository;
        private DateTime _date;
        private IOptions<KafkaProducerSettings> _kafkaSettingsProducer;
        private ILogger<PurcahseProducerService> _logger;

        public PurcahseProducerService(IOptions<KafkaProducerSettings> kafkaSettings, IPurchaseRepository purchaseRepository, ILogger<PurcahseProducerService> logger)
        {
            _kafkaSettingsProducer = kafkaSettings;
            _config = new ProducerConfig()
            {
                BootstrapServers = _kafkaSettingsProducer.Value.BootstrapServers,
            };
            _producer = new ProducerBuilder<Guid, Purchase>(_config)
            .SetKeySerializer(new MsgPackSerializer<Guid>())
            .SetValueSerializer(new MsgPackSerializer<Purchase>())
            .Build();
            _date = DateTime.Now;
            _purchaseRepository = purchaseRepository;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var items = await _purchaseRepository.GetPurchasesAfterDate(_date);

                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                var message = await Execute(item, cancellationToken);
                                await _producer.ProduceAsync(_kafkaSettingsProducer.Value.Topic, message, cancellationToken);
                                Console.WriteLine(message.Value);
                                _date = DateTime.Now;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                    }
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public async Task<Message<Guid, Purchase>> Execute(Purchase purchase, CancellationToken token)
        {
            var msg = new Message<Guid, Purchase>() { Key = purchase.GetKey(), Value = purchase };

            return msg;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}