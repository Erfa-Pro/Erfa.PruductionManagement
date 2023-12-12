using Erfa.ProductionManagement.Application.Services;
using FluentValidation;
using MediatR;
using Moq;

namespace Erfa.ProductionManagement.Application.Test.Unit.Mocks
{
    public class ServiceMocks
    {
        public static Mock<IProductionService> GetProductionService()
        {
            var service = new Mock<IProductionService>();

            service.Setup(service =>
                service.ValidateRequest(It.IsAny<IRequest>(), It.IsAny<AbstractValidator<IRequest>>()))
                    .ReturnsAsync(() =>
                    {
                        return true;
                    });
            return service;
        }
    }
}
