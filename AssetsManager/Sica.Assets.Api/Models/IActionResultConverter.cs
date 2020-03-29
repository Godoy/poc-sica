using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Serilog;
using System.Net;
using Sica.Assets.Borders.Shared;
using Sica.Assets.Shared.Models;

namespace Sica.Assets.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false);
    }

    public class ActionResultConverter : IActionResultConverter
    {
        private readonly string path;

        public ActionResultConverter(IHttpContextAccessor accessor)
        {
            path = accessor.HttpContext.Request.Path.Value;
        }

        public IActionResult Convert<T>(UseCaseResponse<T> response, bool noContentIfSuccess = false)
        {
            if (response == null)
                return BuildError(new[] { new ErrorMessage("000", "ActionResultConverter Error") }, UseCaseResponseKind.InternalServerError);

            if (response.ErrorMessage is null)
            {
                if (noContentIfSuccess)
                {
                    return new NoContentResult();
                }
                else
                {
                    return BuildSuccessResult(response.Result, response.Status);
                }
            }
            else if (response.Result != null)
            {
                return BuildError(response.Result, response.Status);
            }
            else
            {
                var hasErrors = response.Errors == null || !response.Errors.Any();
                var errorResult = hasErrors
                    ? new[] { new ErrorMessage("000", response.ErrorMessage ?? "Unknown error") }
                    : response.Errors;

                return BuildError(errorResult, response.Status);
            }
        }

        private IActionResult BuildSuccessResult(object data, UseCaseResponseKind status)
        {
            switch (status)
            {
                case UseCaseResponseKind.Created:
                    return new CreatedResult($"{path}", data);
                default:
                    return new OkObjectResult(data);
            }
        }

        private ObjectResult BuildError(object data, UseCaseResponseKind status)
        {
            var httpStatus = GetErrorHttpStatusCode(status);
            if (httpStatus == HttpStatusCode.InternalServerError)
            {
                Log.Error($"[ERROR] {path} ({{@data}})", data);
            }

            return new ObjectResult(data)
            {
                StatusCode = (int)httpStatus
            };
        }

        private HttpStatusCode GetErrorHttpStatusCode(UseCaseResponseKind status)
        {
            switch (status)
            {
                case UseCaseResponseKind.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                case UseCaseResponseKind.Forbidden:
                    return HttpStatusCode.Forbidden;
                case UseCaseResponseKind.NotFound:
                    return HttpStatusCode.NotFound;
                case UseCaseResponseKind.BadRequest:
                    return HttpStatusCode.BadRequest;
                case UseCaseResponseKind.OK:
                    return HttpStatusCode.OK;
                case UseCaseResponseKind.Created:
                    return HttpStatusCode.Created;
                case UseCaseResponseKind.Unavailable:
                    return HttpStatusCode.ServiceUnavailable;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
