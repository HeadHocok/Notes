using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Notes.Application.Common.Behaviors;

namespace Notes.Application
{
    /// <summary>
    /// Регистрирует контекст базы данных для медиатора.
    /// Добавляет все необходимые зависимости для работы с MediatR и валидацией команд в приложении.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())); //Регистрирует все сервисы 
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }); //Добавляет все валидаторы из текущей сборки
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)); //представляет поведение, которое выполняет валидацию команд перед их обработкой.
            return services;
        }
    }
}
