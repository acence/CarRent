using CarRent.Application.Behaviours;
using CarRent.WebApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MediatR;
using AutoMapper;

namespace CarRent.WebApi.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<IActionResult> SendAndProcessResponseAsync<TRequest, TResponse>(this IMediator mediator, IMapper mapper,  TRequest request)
        {
            try
            {
                if (request != null)
                {
                    var result = await mediator.Send(request);
                    return new OkObjectResult(mapper.Map<TResponse>(result));
                }
                return new ObjectResult(new ServerErrorResponse { Message = $"Sent null request of type {typeof(TRequest).Name}" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            catch (ValidationException validationEx)
            {
                if (validationEx.Errors.All(x => x.ErrorCode == ValidationErrorCodes.NotFound))
                {
                    return new NotFoundObjectResult(MapErrors(validationEx));
                }
                else
                {
                    return new BadRequestObjectResult(MapErrors(validationEx));
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ServerErrorResponse { Message = ex.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public static  async Task<IActionResult> SendAsync<TRequest>(this IMediator mediator, TRequest request)
        {
            try
            {
                if (request != null)
                {
                    await mediator.Send(request);
                    return new OkResult();
                }
                return new ObjectResult(new ServerErrorResponse { Message = $"Sent null request of type {typeof(TRequest).Name}" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            catch (ValidationException validationEx)
            {
                if (validationEx.Errors.All(x => x.ErrorCode == ValidationErrorCodes.NotFound))
                {
                    return new NotFoundObjectResult(MapErrors(validationEx));
                }
                else
                {
                    return new BadRequestObjectResult(MapErrors(validationEx));
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(new ServerErrorResponse { Message = ex.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        private static IEnumerable<ValidationErrorResponse> MapErrors(ValidationException validationEx)
        {
            return validationEx.Errors.Select(x => new ValidationErrorResponse
            {
                Property = x.PropertyName,
                Message = x.ErrorMessage
            });
        }
    }
}
