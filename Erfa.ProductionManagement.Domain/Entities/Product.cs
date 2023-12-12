using Erfa.ProductionManagement.Domain.Common;

namespace Erfa.ProductionManagement.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public string MaterialProductName { get; set; } = string.Empty;

        public bool Updated(object? obj)
        {
            return obj is Product item &&
                   ProductNumber == item.ProductNumber &&
                   (Description != item.Description ||
                   ProductionTimeSec != item.ProductionTimeSec ||
                   MaterialProductName != item.MaterialProductName);
        }
    }
}
