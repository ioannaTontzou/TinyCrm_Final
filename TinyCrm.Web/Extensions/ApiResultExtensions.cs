using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core;

namespace TinyCrm.Web.Extensions
{
    public static class ApiResultExtensions
    {
        public static ObjectResult AsStatusResult<T>(
            this ApiResult<T> @this)
        {
            if(@this.Success) {
                return new ObjectResult(@this.Data);
            }

            var result = new ObjectResult(@this?.ErrorText);
            result.StatusCode = (int)@this.ErrorCode;

            return result;
        }
    }
}
