using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Catalog.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IAsyncRepository<Product> _catalogRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;

        public CreateProductCommandHandler(
                                    IAsyncRepository<Product> catalogRepository, 
                                    IMapper mapper, 
                                    ProductionService productionService)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<string>  Handle(
                                    CreateProductCommand request, 
                                    CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            Product product = _mapper.Map<Product>(request);
            product.CreatedBy = request.UserName;
            product.LastModifiedBy = request.UserName;

            try
            {
                //TODO send event
                await _catalogRepository.AddAsync(product);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(Product), request);
            }
            return product.ProductNumber;
        }

     
    }
}
