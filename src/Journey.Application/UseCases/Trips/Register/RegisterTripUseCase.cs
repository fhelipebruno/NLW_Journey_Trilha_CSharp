using Journey.Communication.Requests;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public void Execute(RequestRegisterTripJson request)
        {
            Validate(request);
        }

        private void Validate(RequestRegisterTripJson request)
        {
            if (String.IsNullOrWhiteSpace(request.Name))
            {
                throw new JourneyException(ResourceErrorMessages.NOME_INVALIDO_NAO_INFORMADO);
            }

            if(request.StartDate < DateTime.UtcNow)
            {
                throw new JourneyException(ResourceErrorMessages.DATA_INICIO_INVALIDA);
            }

            if (request.EndDate < request.StartDate)
            {
                throw new JourneyException(ResourceErrorMessages.DATA_FIM_MENOR_INICIO);
            }
        }
    }
}
