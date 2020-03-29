using Sica.Assets.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sica.Assets.Borders.Shared
{
    public class UseCaseResponse<T>
    {
        public UseCaseResponseKind Status { get; private set; }
        public string ErrorMessage { get; private set; }
        public IEnumerable<ErrorMessage> Errors { get; private set; }

        public T Result { get; private set; }

        public UseCaseResponse()
        {
            Status = UseCaseResponseKind.OK;
        }

        public UseCaseResponse<T> SetCreated(T result)
        {
            Result = result;
            return SetResult(null, UseCaseResponseKind.Created);
        }

        public UseCaseResponse<T> SetResult(T result)
        {
            Result = result;
            return SetResult(null, UseCaseResponseKind.OK);
        }

        public UseCaseResponse<T> SetError(string errorMessage, UseCaseResponseKind status, IEnumerable<ErrorMessage> errors = null)
        {
            return SetResult(errorMessage, status, errors);
        }

        public UseCaseResponse<T> SetInternalServerError(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.InternalServerError, errors);
        }

        public UseCaseResponse<T> SetUnavailable(T result)
        {
            Result = result;
            Status = UseCaseResponseKind.Unavailable;
            ErrorMessage = "Service Unavailable";
            return this;
        }

        public UseCaseResponse<T> SetBadRequest(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.BadRequest, errors);
        }

        public UseCaseResponse<T> SetBadGateway(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.BadGateway, errors);
        }

        public UseCaseResponse<T> SetNotFound(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.NotFound, errors);
        }

        public bool Success()
        {
            return ErrorMessage is null;
        }

        private UseCaseResponse<T> SetResult(string errorMessage, UseCaseResponseKind status, IEnumerable<ErrorMessage> errors = null)
        {
            ErrorMessage = errorMessage;
            Status = status;
            Errors = errors;

            return this;
        }
    }    
}
