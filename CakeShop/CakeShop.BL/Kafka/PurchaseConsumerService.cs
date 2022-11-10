using CakeShop.DL.Interfaces;
using CakeShop.DL.MongoRepositories;
using CakeShop.MessagePack;
using CakeShop.Models.Models.Configurations;
using CakeShop.Models.ModelsMongoDB;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks.Dataflow;

namespace CakeShop.BL.Kafka
{
    public class PurchaseConsumerService : IHostedService
    {
        IPurchaseRepository _purchaseRepository;
        private IProcessedPurchasesRepository _processedPurchases;
        private CancellationTokenSource _cancellationTokenSource;
        TransformBlock<Purchase, Purchase> transferBlockPurchase;
        private IOptions<KafkaConsumerSettings> _kafkaSettings;
        private ConsumerConfig _consumerConfig;
        public IConsumer<Guid, Purchase> _consumer;
        private ILogger<PurchaseConsumerService> _logger;

        public PurchaseConsumerService(IOptions<KafkaConsumerSettings> kafkaSettings, IPurchaseRepository purchaseRepository, ILogger<PurchaseConsumerService> logger, IProcessedPurchasesRepository processedPurchases)
        {
            _kafkaSettings = kafkaSettings;
            _purchaseRepository = purchaseRepository;

            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset?)_kafkaSettings.Value.AutoOffsetReset,
                GroupId = _kafkaSettings.Value.GroupId
            };

            _consumer = new ConsumerBuilder<Guid, Purchase>(_consumerConfig)
                    .SetKeyDeserializer(new MsgPackDeserializer<Guid>())
                    .SetValueDeserializer(new MsgPackDeserializer<Purchase>())
                    .Build();

            _consumer.Subscribe(_kafkaSettings.Value.Topic);

            _cancellationTokenSource = new CancellationTokenSource();
            _logger = logger;
            _processedPurchases = processedPurchases;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            transferBlockPurchase = new TransformBlock<Purchase, Purchase>(p =>
            {
                Purchase purchase = null;
                if (p != null && p.Cakes.Count() > 0)
                {
                    purchase = _purchaseRepository.GetPurchasesById(p.Id).Result;
                    _logger.LogInformation("Cakes count: " + purchase.Cakes.Count() + ", Total money: " + purchase.TotalMoney);
                }
                return purchase;
            });

            var actionBlock = new ActionBlock<Purchase>(p =>
            {
                if (p != null)
                {
                    _processedPurchases.AddProcessedPurchase(p);
                    _logger.LogInformation($"Purchase with id: {p.Id} was processed.");
                }
            });

            transferBlockPurchase.LinkTo(actionBlock);

            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        var cr = _consumer.Consume();
                        HandleMessage(cr.Message.Value);

                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                    }
                }
            });

            return Task.CompletedTask;
        }

        public void HandleMessage(Purchase purchase)
        {
            transferBlockPurchase.SendAsync(purchase);
        }

        public void Consume(Purchase purchase)
        {
            transferBlockPurchase.SendAsync(purchase);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
