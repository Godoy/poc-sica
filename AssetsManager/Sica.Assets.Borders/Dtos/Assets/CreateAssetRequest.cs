using Sica.Assets.Borders.Entities;
using System;

namespace Sica.Assets.Borders.Dtos.Assets
{
    public class CreateAssetRequest
    {
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime PurchasedAt { get; set; }
        public DateTime MaintenanceOn { get; set; }
        
        public Asset ToAsset()
        {
            return ToAsset(Guid.NewGuid());
        }

        public Asset ToAsset(Guid id)
        {
            return new Asset()
            {
                Id = id,
                Model = Model,
                Description = Description,
                PurchasedAt = PurchasedAt,
                MaintenanceOn = MaintenanceOn
            };
        }
    }
}