﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Demo.Common.Middleware
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }  
        public Func<Exception, LogLevel> DetermineLogLevel { get; set; }
    }
}
