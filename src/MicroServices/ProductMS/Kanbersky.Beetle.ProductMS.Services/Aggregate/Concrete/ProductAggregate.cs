using Kanbersky.Beetle.Core.EventModels.Product;
using Kanbersky.Beetle.ProductMS.Services.Aggregate.Abstract;
using Kanbersky.Beetle.ProductMS.Services.DTO.Request;
using System;

namespace Kanbersky.Beetle.ProductMS.Services.Aggregate
{
    public class ProductAggregate : Infrastructure.EventSourcing.Concrete.Aggregate , IProductAggregate
    {
        public CreateProductRequestModel CreateProductRequestModel { get; private set; }

        public ProductAggregate() { }

        public ProductAggregate CreateProduct(CreateProductRequestModel createProductRequestModel)
        {
            var @event = new ProductCreatedEventModel
            {
                Name = createProductRequestModel.Name,
                Price = createProductRequestModel.Price,
                Quantity = createProductRequestModel.Quantity
            };

            Apply(@event);

            return new ProductAggregate
            {
                CreateProductRequestModel = createProductRequestModel,
                CreatedDate = @event.CreatedDate,
                Id = @event.Id,
                Version = 1
            };
        }

        public void DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
