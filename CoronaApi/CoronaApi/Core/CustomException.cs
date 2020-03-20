using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace CoronaApi.Core
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly IWebHostEnvironment _environment;
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            this._next = next;
            this._environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this._next(context)
                          .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex)
                          .ConfigureAwait(false);
            }
        }

        private static string PropertyFromKey(string key)
        {
            if (!key.Contains("."))
            {
                return key;
            }

            var retValue = key.Substring(key.LastIndexOf(".", StringComparison.Ordinal) + 1);
            return retValue;
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new {error = validationException.Failures});
                    break;
                case ArgumentNullException argumentNullException when argumentNullException.Source == "MediatR" && argumentNullException.ParamName == "request":
                    var fails = new Dictionary<string, IEnumerable<string>>();
                    fails.Add("body", new[] {"JSON body could not be binded to model"});
                    result = JsonConvert.SerializeObject(fails);
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;

            // We probaly want to see the underlying error in development like missing table in database etc
            if (this._environment.IsDevelopment() || this._environment.IsStaging())
            {
                if (string.IsNullOrEmpty(result))
                {
                    var errorList = new List<string> {$"{exception.GetType()}: {exception.Message}"};
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                        errorList.Add($"{exception.GetType()}: {exception.Message}");
                    }

                    result = JsonConvert.SerializeObject(new {error = errorList});
                }
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            this.Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                                .Select(e => e.PropertyName)
                                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                                       .Where(e => e.PropertyName == propertyName)
                                       .Select(e => e.ErrorMessage)
                                       .ToArray();

                this.Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}