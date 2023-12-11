using AutoMapper;
using Erfa.ProductionManagement.Application.Test.Unit.Mocks;
using Erfa.ProductionManagement.Application.Contracts.Persistence;
using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
using Erfa.ProductionManagement.Application.Services;
using Erfa.ProductionManagement.Domain.Entities;
using Shouldly;
using Moq;
using Erfa.ProductionManagement.Application.Exceptions;
using Erfa.ProductionManagement.Application.Profiles;

namespace Erfa.ProductionManagement.Application.Test.Unit.Catalog.Commands
{
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IAsyncRepository<Product>> _mockCatalogRepository;
        private readonly IMapper _mapper;
        private Mock<IProductionService> _mockProductionService;
        private CreateProductCommandHandler SUT;

        public CreateProductCommandHandlerTest()
        {
            _mockCatalogRepository = RepositoryMocks.GetIAsyncCatalogRepository();
            _mockProductionService = ServiceMocks.GetProductionService();
            var _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductMappingProfile>();
            });
            _mapper = _mapperConfiguration.CreateMapper();
        }

        [SetUp]
        public void setup()
        {
            SUT = new CreateProductCommandHandler(_mockCatalogRepository.Object, _mapper, _mockProductionService.Object);
        }

        [Test]
        public async Task GivenValidCommand_WhenCreateProduct_ProductIsSavedInDatabase()
        {
            var command = new CreateProductCommand(RepositoryMocks._validCommand, RepositoryMocks._UserName);

            var result = await SUT.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<string>();
            result.ShouldBe(RepositoryMocks._ProductNumber);
            _mockCatalogRepository.Verify();
            _mockProductionService.Verify();
        }

        [Test]
        public async Task GivenExistingProductNumber_WhenCreateProduct_PersistenceExceptionIsThrown()
        {
            var command = new CreateProductCommand(RepositoryMocks._existingProductNumberCreateProductCommand, RepositoryMocks._UserName);

            await Should.ThrowAsync<PersistenceFailedException>(async () => await SUT.Handle(command, CancellationToken.None));
        }
    }
}
