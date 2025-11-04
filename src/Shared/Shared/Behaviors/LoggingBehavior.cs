using MediatR;

using Microsoft.Extensions.Logging;

using System.Diagnostics;

namespace Shared.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest
        where TResponse : notnull
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request {Request} - Response={Response} Request Data={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = next();

            timer.Stop();
            var timeTake = timer.Elapsed;
            if (timeTake.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}",
                    typeof(TRequest).Name, timeTake.Seconds);
            }

            logger.LogInformation("[END] Handled request {Request} with response {Response}",
                typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
