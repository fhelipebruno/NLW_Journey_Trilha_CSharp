using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);

            var dbContext = new JourneyDbContext();
            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate, 
                EndDate = request.EndDate
            };

            dbContext.Trips.Add(entity);
            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                Name = entity.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
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
