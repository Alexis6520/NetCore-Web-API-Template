using Application.Wrappers;
using FluentValidation;
using MediatR;
using System.Net;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
        IPipelineBehavior<TRequest, TResponse> where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);
                var results = await Task.WhenAll(_validators.Select(val => val.ValidateAsync(validationContext)));

                var errors = results.Where(x => !x.IsValid)
                    .SelectMany(x => x.Errors)
                    .ToList();

                if (errors.Count > 0)
                {
                    var messages = errors.Select(x => x.ErrorMessage).ToList();
                    var result = Activator.CreateInstance<TResponse>();
                    result.Succeeded = false;
                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.Errors = messages;
                    return result;
                }
            }

            return await next();
        }
    }
}
