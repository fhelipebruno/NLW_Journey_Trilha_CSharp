using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.NOME_INVALIDO_NAO_INFORMADO);

            RuleFor(request => request.StartDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(ResourceErrorMessages.DATA_INICIO_INVALIDA);

            RuleFor(request => request)
                .Must(request => request.EndDate >= request.StartDate)
                .WithMessage(ResourceErrorMessages.DATA_FIM_MENOR_INICIO);
        }
    }
}
