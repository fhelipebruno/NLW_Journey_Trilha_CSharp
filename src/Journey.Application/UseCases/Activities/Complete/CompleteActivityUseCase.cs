using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Activities.Complete
{
    public class CompleteActivityUseCase
    {
        public void Execute (Guid TripId, Guid ActivityId)
        {
            var context = new JourneyDbContext();
            var entity = context.Activities.FirstOrDefault(activity => activity.TripId == TripId && activity.Id == ActivityId);

            if(entity is null)
            {
                throw new NotFoundException(ResourceErrorMessages.ATIVIDADE_NAO_ENCONTRADA);
            }

            entity.Status = ActivityStatus.Done;

            context.Activities.Update(entity);
            context.SaveChanges();
        }
    }
}
