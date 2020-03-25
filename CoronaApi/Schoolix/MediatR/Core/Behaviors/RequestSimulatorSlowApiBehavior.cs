using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Schoolix.MediatR.Core.Behaviors
{
    /// <summary>
    ///     Can be used to slow down whole handling of request.
    ///     Sometimes this is requested to test stuff like mobile internet or loading animation on client side that depends on
    ///     http interceptors
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class RequestSimulatorSlowApiBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            return await next();
        }
    }
}