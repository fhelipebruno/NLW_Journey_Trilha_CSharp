using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Journey.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityUseCase
    {
        public ResponseActivityJson Execute(Guid TripId, RequestRegisterActivityJson request)
        {
            var context = new JourneyDbContext();
            var trip = context
                            .Trips
                            .FirstOrDefault(trip => trip.Id == TripId);            

            Validate(trip, request);

            var entity = new Activity{
                Name = request.Name,
                Date = request.Date,
                TripId = TripId
            };

            context.Activities.Add(entity);
            context.SaveChanges();

            return new ResponseActivityJson
            {
                Date = entity.Date,
                Id = entity.Id,
                Name = entity.Name,
                Status = (Communication.Enums.ActivityStatus)entity.Status
            };
        }

        private void Validate(Trip? trip, RequestRegisterActivityJson request)
        {
            if (trip is null)
            {
                throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);
            }

            var validator = new RegisterActivityValidator();
            var result = validator.Validate(request);

            if((request.Date >= trip.StartDate && request.Date <= trip.EndDate) == false)
            {
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATA_ATIVIDADE_FORA_PERIODO_VIAGEM));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
