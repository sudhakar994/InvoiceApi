﻿using InvoiceApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace InvoiceApi
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILoggerManager _logger;
        public ExceptionMiddleware(RequestDelegate next)
        {
            //_logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string ErrorMessgage = exception.Message;

            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(exception, true);

            string pagename = trace.GetFrame((trace.FrameCount - 1)).GetFileName();

            string method = trace.GetFrame((trace.FrameCount - 1)).GetMethod().ToString();

            Int32 lineNumber = trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber();

            var path = context.Request.Path.Value;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());


        }
    }
}
