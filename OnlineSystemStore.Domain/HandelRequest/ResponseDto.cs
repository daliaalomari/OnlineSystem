using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystemStore.Domain.HandelRequest
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string? UserMessageAr { get; set; }
        public string? UserMessageEng { get; set; }
        public string? ErrorMessage { get; set; }
        public Exception? Exception { get; set; }
        public T? Data { get; set; }
        public int? StatusCode { get; set; }
        public long ExecutionTimeInMilliseconds { get; set; }

        private ResponseDto(
        bool success,
        string? UserMessageAr = null,
        string? UserMessageEng = null,
        string? errorMessage = null,
        Exception? exception = null,
        T? data = default,
        int? statusCode = null,
        long executionTimeInMilliseconds = 0)
        {
            Success = success;
            ErrorMessage = errorMessage;
            Exception = exception;
            Data = data;
            StatusCode = statusCode;
            ExecutionTimeInMilliseconds = executionTimeInMilliseconds;
        }

        public static ResponseDto<T> SuccessResponse(
            T? data = default,
            string? userMessageAr = "تمت العملية بنجاح",
            string? userMessageEng = "Operation succeeded",
             int? statusCode = 200,
             long executionTimeInMilliseconds = 0)
        {
            return new ResponseDto<T>(true,
                data: data,
                UserMessageAr: userMessageAr,
                UserMessageEng: userMessageEng,
                statusCode: statusCode,
                executionTimeInMilliseconds: executionTimeInMilliseconds);

        }

        public static ResponseDto<T> WarningResponse(
            string? userMessageAr = "تمت العملية بنجاح ولكن توجد مشكلة",
            string? userMessageEng = "Operation completed with warnings",
            T? data = default,
            long executionTime = 0)
        {
            return new ResponseDto<T>(true,
                UserMessageAr: userMessageAr,
                UserMessageEng: userMessageEng,
                data: data,
                executionTimeInMilliseconds: executionTime);
        }

        public static ResponseDto<T> ErrorResponse(
        string? userMessageAr = "حصلة مشكلة أثناء تنفيذ العملية",
        string? userMessageEng = "An error occurred",
        string? errorMessage = "An error occurred.",
        Exception? exception = null,
        int? statusCode = 500,
        long executionTime = 0)
        {
            return new ResponseDto<T>(
                false,
                UserMessageAr: userMessageAr,
                UserMessageEng: userMessageEng,
                errorMessage: errorMessage,
                exception: exception,
                statusCode: statusCode,
                executionTimeInMilliseconds: executionTime);
        }

    }
}
