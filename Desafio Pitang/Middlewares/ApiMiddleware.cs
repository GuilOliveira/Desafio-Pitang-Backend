using DesafioPitang.Repository.Interface;
using DesafioPitang.Utils.Attributes;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;
using DesafioPitang.Utils.Responses;
using log4net;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace DesafioPitang.WebApi.Middlewares
{
    public class ApiMiddleware : IMiddleware
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ApiMiddleware));
        private readonly ITransactionManager _transactionManager;
        public ApiMiddleware(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var requiredTransaction = httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<RequiredTransactionAttribute>();

            try
            {
                if (requiredTransaction != null)
                {
                    await _transactionManager.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted).ConfigureAwait(false);
                    await next.Invoke(httpContext);
                    await _transactionManager.CommitTransactionsAsync();
                }
                else
                {
                    await next.Invoke(httpContext);
                }

                stopwatch.Stop();
                _log.InfoFormat("Serviço executado com sucesso: {0} {1} [{2} ms]", httpContext.Request.Method, httpContext.Request.Path, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                if (requiredTransaction != null)
                {
                    await _transactionManager.RollbackTransactionsAsync();
                }
                
                stopwatch.Stop();
                _log.Error($"Erro no serviço: {httpContext.Request.Path} / Mensagem: {ex.Message} [{stopwatch.ElapsedMilliseconds}]", ex);
                await HandleException(httpContext, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception exception)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            await response.WriteAsync(JsonConvert.SerializeObject(new DefaultResponse(HttpStatusCode.InternalServerError, GetMessages(exception))));
        }
        private static List<string> GetMessages(Exception exception)
        {
            var messages = new List<string>();

            switch (exception)
            {
                case BusinessException:
                    messages.Add(exception.Message);
                    break;
                default:
                    messages.Add(string.Format(InfraMessages.UnexpectedError));
                    break;
            }

            return messages;
        }
    }
}
