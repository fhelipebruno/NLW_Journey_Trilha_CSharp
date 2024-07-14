using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class DeleteByIdTripUseCase
    {
        public void Execute(Guid id)
        {
            var context = new JourneyDbContext();
            var entity = context
                            .Trips
                            .Include(trip => trip.Activities)
                            .FirstOrDefault(trip => trip.Id == id);

            if(entity is null)
            {
                throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);
            }

            context.Trips.Remove(entity);
            context.SaveChanges();
        }
    }
}
