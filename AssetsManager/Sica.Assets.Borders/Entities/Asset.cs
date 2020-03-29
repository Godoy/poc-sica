using System;

namespace Sica.Assets.Borders.Entities
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime PurchasedAt { get; set; }
        public DateTime MaintenanceOn { get; set; }
    }
}
