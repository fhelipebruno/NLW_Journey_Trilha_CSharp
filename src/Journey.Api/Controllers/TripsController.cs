using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Delete;
using Journey.Application.UseCases.Activities.Register;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {
            var useCase = new RegisterTripUseCase();
            var response = useCase.Execute(request);

            return Created(String.Empty, response);
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var useCase = new GetAllTripUseCase();
            var response = useCase.Execute();

            return Ok(response);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var useCase = new GetByIdTripUseCase();
            var response = useCase.Execute(id);

            return Ok(response);
            
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var useCase = new DeleteByIdTripUseCase();
            useCase.Execute(id);

            return NoContent();

        }

        [HttpPost]
        [Route("{TripId}/Activity")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterActivity([FromRoute] Guid TripId, [FromBody] RequestRegisterActivityJson request)
        {
            var useCase = new RegisterActivityUseCase();
            var response = useCase.Execute(TripId, request);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{TripId}/Activity/{ActivityId}/Complete")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status500InternalServerError)]
        public IActionResult CompleteActivity([FromRoute] Guid TripId, [FromRoute] Guid ActivityId)
        {
            var useCase = new CompleteActivityUseCase();
            useCase.Execute(TripId, ActivityId);

            return NoContent();
        }

        [HttpDelete]
        [Route("{TripId}/Activity/{ActivityId}")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrosJson), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteActivity([FromRoute] Guid TripId, [FromRoute] Guid ActivityId)
        {
            var useCase = new DeleteActivityUseCase();
            useCase.Execute(TripId, ActivityId);

            return NoContent();
        }
    }
}
