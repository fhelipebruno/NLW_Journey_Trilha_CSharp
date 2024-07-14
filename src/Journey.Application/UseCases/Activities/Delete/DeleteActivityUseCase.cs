using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure.Entities;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Activities.Delete
{
    public class DeleteActivityUseCase
    {
        public void Execute(Guid tripId, Guid activityId)
        {
            var context = new JourneyDbContext();
            var entity = context.Activities.FirstOrDefault(activity => activity.TripId == tripId && activity.Id == activityId);

            if (entity is null)
            {
                throw new NotFoundException(ResourceErrorMessages.ATIVIDADE_NAO_ENCONTRADA);
            }

            context.Activities.Remove(entity);
            context.SaveChanges();
        }
    }
}
