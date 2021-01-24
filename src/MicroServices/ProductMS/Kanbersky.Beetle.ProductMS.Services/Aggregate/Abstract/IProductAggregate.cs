using Kanbersky.Beetle.ProductMS.Services.DTO.Request;
using System;

namespace Kanbersky.Beetle.ProductMS.Services.Aggregate.Abstract
{
    public interface IProductAggregate
    {
        ProductAggregate CreateProduct(CreateProductRequestModel createProductRequestModel);

        void DeleteProduct(Guid id);
    }
}
