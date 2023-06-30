using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;

namespace Notes.Application.Common.Behaviors
{
    /// <summary>
    /// Встраивает реализацию пайплайн-поведения для валидации запросов в приложении. 
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> 
        //where TRequest реализует TResponse, гарантируя необходимую структуру для дальнейшей работы.
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators; //принимает коллекцию валидаторов.

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) =>
            _validators = validators;

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context)) //вызывается для каждого валидатора. Выполняется параллельно.
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();
            if (failures.Count != 0) //если возникли ошибки в ходе метода - выбрасываем исключения.
            {
                throw new ValidationException(failures);
            }
            return next();
        }
    }
}