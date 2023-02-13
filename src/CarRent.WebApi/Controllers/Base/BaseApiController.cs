using CarRent.Application.Behaviours;
using CarRent.WebApi.Models.Response;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CarRent.WebApi.Controllers.Base
{
    public abstract class BaseApiController : ControllerBase
    {
        private readonly ILogger _logger;

        public BaseApiController(ILogger logger)
        {
            _logger = logger;
        }
        protected async Task<IActionResult> ProcessResponse<T>(Func<Task<T>> function)
        {
            try
            {
                return Ok(await function());
            }
            catch (ValidationException validationEx)
            {
                if (validationEx.Errors.All(x => x.ErrorCode == ValidationErrorCodes.NotFound))
                {
                    return NotFound(MapErrors(validationEx));
                }
                else
                {
                    return BadRequest(MapErrors(validationEx));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ServerErrorResponse { Message = ex.Message });
            }
        }

        protected async Task<IActionResult> ProcessResponse(Func<Task> function)
        {
            try
            {
                await function();
                return Ok();
            }
            catch (ValidationException validationEx)
            {
                if (validationEx.Errors.All(x => x.ErrorCode == ValidationErrorCodes.NotFound))
                {
                    return NotFound(MapErrors(validationEx));
                }
                else
                {
                    return BadRequest(MapErrors(validationEx));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ServerErrorResponse { Message = ex.Message });
            }
        }

        private IEnumerable<ValidationErrorResponse> MapErrors(ValidationException validationEx)
        {
            return validationEx.Errors.Select(x => new ValidationErrorResponse
            {
                Property = x.PropertyName,
                Message = x.ErrorMessage
            });
        }
    }
}
