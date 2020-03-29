using FluentValidation;
using Microsoft.Extensions.Logging;
using Sica.Assets.Borders.Dtos.Assets;
using Sica.Assets.Borders.Entities;
using Sica.Assets.Borders.Repositories;
using Sica.Assets.Borders.Shared;
using Sica.Assets.Borders.UseCases.Assets;
using Sica.Assets.Borders.Validators;
using Sica.Assets.Shared.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text;

namespace Sica.Assets.UseCases.Policy
{
    public class CreateAssetUseCase : ICreateAssetUseCase
    {
        private readonly IAssetRepository assetRepository;
        private readonly CreateAssetRequestValidator assetValidator;
        private readonly ILogger<CreateAssetUseCase> logger;

        public CreateAssetUseCase(IAssetRepository assetRepository,
            CreateAssetRequestValidator assetValidator,
            ILogger<CreateAssetUseCase> logger)
        {
            this.assetRepository = assetRepository;
            this.assetValidator = assetValidator;         
            this.logger = logger;
        }

        public async Task<UseCaseResponse<Asset>> Execute(CreateAssetRequest request)
        {
            var response = new UseCaseResponse<Asset>();
            try
            {
                assetValidator.ValidateAndThrow(request);
                var asset = request.ToAsset();
             
                var createdAsset = await assetRepository.Create(asset);

                sendMessage(asset);


                return response.SetCreated(createdAsset);
            }
            catch (ValidationException ex)
            {
                return response.SetBadRequest("Validation exception", ex.ToErrorMessage().ToArray());
            }
            catch (Exception e)
            {                
                logger.LogError(e.Message, e);
                return response.SetInternalServerError("Unexpected error: "+ e.Message);
            }
        }

        private void sendMessage(Asset asset)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "user", Password = "bitnami" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "asset_maintenance",
                                                     durable: false,
                                                     exclusive: false,
                                                     autoDelete: false,
                                                     arguments: null);

                    string message = $"Manutencao necessaria no modelo {asset.Model} em {asset.MaintenanceOn}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "asset_maintenance",
                                         basicProperties: null,
                                         body: body);
                }
            }
        }
    }
}
