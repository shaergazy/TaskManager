﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Infrastructure;

internal class NoContentFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor actionDescriptor)
            return;

        var returnType = actionDescriptor.MethodInfo.ReturnType;
        if (returnType == typeof(void) || returnType == typeof(Task))
            context.HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}
