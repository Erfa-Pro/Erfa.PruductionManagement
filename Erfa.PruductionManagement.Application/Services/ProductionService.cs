using Erfa.PruductionManagement.Application.Exceptions;
using FluentValidation;
using System.IdentityModel.Tokens.Jwt;

namespace Erfa.PruductionManagement.Application.Services
{
    public class ProductionService
    {
        internal async Task<bool> ValidateRequest<TR>(TR request, AbstractValidator<TR> validator)
        {
            var validationResults = await validator.ValidateAsync(request);
            if (validationResults.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResults);
            }
            return true;
        }
    }
}
