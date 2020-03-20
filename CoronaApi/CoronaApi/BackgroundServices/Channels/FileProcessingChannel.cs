﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CoronaApi.BackgroundServices.Channels
{
    public class FileProcessingChannel
    {
        private const int MaxMessagesInChannel = 100;

        private readonly Channel<string> _channel;
        private readonly ILogger<FileProcessingChannel> _logger;

        public FileProcessingChannel(ILogger<FileProcessingChannel> logger)
        {
            var options = new BoundedChannelOptions(MaxMessagesInChannel)
            {
                SingleWriter = false,
                SingleReader = true
            };

            this._channel = Channel.CreateBounded<string>(options);

            this._logger = logger;
        }

        public async Task<bool> AddFileAsync(string fileName, CancellationToken ct = default)
        {
            while (await this._channel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                if (this._channel.Writer.TryWrite(fileName))
                {
                    Log.ChannelMessageWritten(this._logger, fileName);

                    return true;
                }
            }

            return false;
        }

        public IAsyncEnumerable<string> ReadAllAsync(CancellationToken ct = default) =>
            this._channel.Reader.ReadAllAsync(ct);

        public bool TryCompleteWriter(Exception ex = null) => this._channel.Writer.TryComplete(ex);

        internal static class EventIds
        {
            public static readonly EventId ChannelMessageWritten = new EventId(100, "ChannelMessageWritten");
        }

        private static class Log
        {
            private static readonly Action<ILogger, string, Exception> _channelMessageWritten = LoggerMessage.Define<string>(
                LogLevel.Information,
                EventIds.ChannelMessageWritten,
                "Filename {FileName} was written to the channel.");

            public static void ChannelMessageWritten(ILogger logger, string fileName)
            {
                _channelMessageWritten(logger, fileName, null);
            }
        }
    }
}