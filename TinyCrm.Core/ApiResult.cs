using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core
{
   public class ApiResult<T>
    {
        public StatusCodecs ErrorCode { get; set; }

        public string ErrorText { get; set; }

        public T Data { get; set; }

        public bool Success => ErrorCode == StatusCodecs.OK;

        public ApiResult()
        {

        }

        public ApiResult(StatusCodecs errorCode, string errorMsg)
        {
            ErrorCode = errorCode;
            ErrorText = errorMsg;
        }

        public  ApiResult<U> GetApi<U>()
        {

          return  new ApiResult<U>()
            {
                ErrorCode = ErrorCode,
                ErrorText = ErrorText
            };
        }

        public static ApiResult<T> CreateSuccess(T data)
        {
            return new ApiResult<T>()
            {
                ErrorCode = StatusCodecs.OK,
                ErrorText = "OK",
                Data = data
            };

        }

    }

}
