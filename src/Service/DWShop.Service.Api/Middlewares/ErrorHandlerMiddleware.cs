using DWShop.Shared.Wrapper;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace DWShop.Service.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public ErrorHandlerMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = await Result<string>.FailAsync(error.Message);

                switch (error) 
                {
                    case ValidationException e:
                        var messages = e.Errors.Select(x => x.ErrorMessage).ToList();
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel = await Result<string>.FailAsync(messages);
                    break;
                
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
