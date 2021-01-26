using FluentValidation;
using Kanbersky.Beetle.Infrastructure.EventSourcing.Abstract;
using Kanbersky.Beetle.ProductMS.Services.Aggregate;
using Kanbersky.Beetle.ProductMS.Services.Aggregate.Abstract;
using Kanbersky.Beetle.ProductMS.Services.DTO.Request;
using Kanbersky.Beetle.ProductMS.Services.DTO.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.ProductMS.Services.Commands
{
    public class CreateProductCommand : IRequest<CreateProductResponseModel>
    {
        public CreateProductRequestModel CreateProductRequestModel { get; set; }

        public CreateProductCommand(CreateProductRequestModel createProductRequestModel)
        {
            CreateProductRequestModel = createProductRequestModel;
        }
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductRequestModel>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(100);

            RuleFor(c => c.Quantity)
                .GreaterThan(0);

            RuleFor(c => c.Price)
                .GreaterThan(0);
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponseModel>
    {
        private readonly IRepository<ProductAggregate> _repository;
        private readonly IProductAggregate _productAggregate;

        public CreateProductCommandHandler(IRepository<ProductAggregate> repository,
            IProductAggregate productAggregate)
        {
            _repository = repository;
            _productAggregate = productAggregate;
        }

        public async Task<CreateProductResponseModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _productAggregate.CreateProduct(request.CreateProductRequestModel);
            await _repository.Add(product);

            return new CreateProductResponseModel
            {

            };
        }
    }
}
