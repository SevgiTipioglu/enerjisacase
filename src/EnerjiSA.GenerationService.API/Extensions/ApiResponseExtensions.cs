using EnerjiSA.GenerationService.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.API.Extensions
{
    public static class ApiResponseExtensions
    {
        public static IActionResult ToHttpResponse(this ServiceResponse response)
        {
            var status = HttpStatusCode.OK;

            if (response.ValidationErrors.Any())
                status = HttpStatusCode.BadRequest;
            else if (response.Success == false)
                status = HttpStatusCode.InternalServerError;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

    }
}
