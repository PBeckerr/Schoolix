using System;
using System.Threading;
using System.Threading.Tasks;
using Schoolix.BackgroundServices.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Schoolix.BackgroundServices
{
    public class FileProcessingService : BackgroundService
    {
        private readonly FileProcessingChannel _fileProcessingChannel;
        private readonly ILogger<FileProcessingService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public FileProcessingService(
            ILogger<FileProcessingService> logger,
            FileProcessingChannel boundedMessageChannel,
            IServiceProvider serviceProvider)
        {
            this._logger = logger;
            this._fileProcessingChannel = boundedMessageChannel;
            this._serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var fileName in this._fileProcessingChannel.ReadAllAsync(stoppingToken))
            {
                using var scope = this._serviceProvider.CreateScope();
                //do smth with the file
                Log.StartedProcessing(this._logger);
                Log.StoppedProcessing(this._logger);
            }
        }

        internal static class EventIds
        {
            public static readonly EventId StartedProcessing = new EventId(100, "StartedProcessing");
            public static readonly EventId ProcessorStopping = new EventId(101, "ProcessorStopping");
            public static readonly EventId StoppedProcessing = new EventId(102, "StoppedProcessing");
            public static readonly EventId ProcessedMessage = new EventId(110, "ProcessedMessage");
        }

        private static class Log
        {
            private static readonly Action<ILogger, string, Exception> _processedMessage = LoggerMessage.Define<string>(
                LogLevel.Debug,
                EventIds.ProcessedMessage,
                "Read and processed message with ID '{MessageId}' from the channel.");

            public static void StartedProcessing(ILogger logger)
            {
                logger.Log(LogLevel.Information, EventIds.StartedProcessing, "Started message processing service.");
            }

            public static void ProcessorStopping(ILogger logger)
            {
                logger.Log(LogLevel.Information, EventIds.ProcessorStopping, "Message processing stopping due to app termination!");
            }

            public static void StoppedProcessing(ILogger logger)
            {
                logger.Log(LogLevel.Information, EventIds.StoppedProcessing, "Stopped message processing service.");
            }

            public static void ProcessedMessage(ILogger logger, string messageId)
            {
                _processedMessage(logger, messageId, null);
            }
        }
    }
}