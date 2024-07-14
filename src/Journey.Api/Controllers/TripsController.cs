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
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {
            try
            {
                var useCase = new RegisterTripUseCase();

                var response = useCase.Execute(request);

                return Created(String.Empty, response);
            }
            catch (JourneyException ex)
            {
                Console.WriteLine("Ocorreu erro ao processar a requisição: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
            }
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                var useCase = new GetAllTripUseCase();
                var response = useCase.Execute();

                return Ok(response);
            }
            catch (JourneyException ex)
            {
                Console.WriteLine("Ocorreu erro ao processar a requisição: " + ex.Message);
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var useCase = new GetByIdTripUseCase();
                var response = useCase.Execute(id);

                return Ok(response);
            }
            catch (JourneyException ex)
            {
                Console.WriteLine("Ocorreu erro ao processar a requisição: " + ex.Message);
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
            }
        }
    }
}
