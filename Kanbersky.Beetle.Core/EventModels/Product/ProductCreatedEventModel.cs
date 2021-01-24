using System;

namespace Kanbersky.Beetle.Core.EventModels.Product
{
    public class ProductCreatedEventModel
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTime CreatedDate { get; } = DateTime.Now;

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
