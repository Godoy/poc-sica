using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Sica.Assets.Shared.Models;

namespace Sica.Assets.Shared.Extensions
{
    public static class ValidationFailureExtension
    {
        public static IEnumerable<ErrorMessage> ToErrorMessage(this ValidationException exception)
        {
            return exception.Errors?.Select(error => new ErrorMessage(error.ErrorCode, error.ErrorMessage));
        }

        public static IEnumerable<ErrorMessage> ToErrorMessage(this IList<ValidationFailure> errors)
        {
            var response = new List<ErrorMessage>();

            if (errors == null || !errors.Any())
                return response;

            response.AddRange(
                errors.Select(error => new ErrorMessage(error.ErrorCode, error.ErrorMessage)));

            return response;
        }
    }
}
