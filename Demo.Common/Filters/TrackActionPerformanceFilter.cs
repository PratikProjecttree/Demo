﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Demo.Common.Attributes;
using Demo.Common.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Demo.Common.Filters
{
    public class TrackActionPerformanceFilter : IActionFilter
    {
        private Stopwatch _timer;
        private readonly ILogger<TrackActionPerformance> _logger;
        private IDisposable _userScope;

        public TrackActionPerformanceFilter(ILogger<TrackActionPerformance> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _timer = new Stopwatch();

            var userDict = new Dictionary<string, string>
            {
                {"UserId", context.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value},
                {
                    "OAuth2 Scopes",
                    string.Join(",", context.HttpContext.User.Claims.Where(c => c.Type == "scope").Select(c => c.Value))
                },
                {"ClientIP", context.HttpContext.Connection.RemoteIpAddress?.ToString()},
                {"UserAgent", context.HttpContext.Request.Headers["User-Agent"]}
            };
            _userScope = _logger.BeginScope(userDict);
            _timer.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            if (context.Exception == null)
            {
                _logger.LogRoutePerformance(context.HttpContext.Request.Path, context.HttpContext.Request.Method,
                    _timer.ElapsedMilliseconds);
            }

            _userScope?.Dispose();
        }
    }
}