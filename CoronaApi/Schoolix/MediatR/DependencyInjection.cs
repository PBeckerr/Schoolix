using System.Reflection;
using AutoMapper;
using Schoolix.BackgroundServices;
using Schoolix.BackgroundServices.Channels;
using Schoolix.MediatR.Core.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Schoolix.MediatR
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            // Can be enabled to support slow api
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestSimulatorSlowApiBehavior<,>));

            services.AddSingleton<FileProcessingChannel>();
            services.AddHostedService<FileProcessingService>();

            return services;
        }
    }
}