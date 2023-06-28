using System;

namespace WebApi.BusinessLogic.Contracts.Exceptions
{
    public class InvalidModelException : Exception
    {
        public string ErrorCode { get; }

        public InvalidModelException(
            string errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}