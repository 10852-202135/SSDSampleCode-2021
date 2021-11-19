using L09Logging.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace L09Logging.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            string logMsg;
            string requestID = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            if (statusCode.HasValue)
            {
                string OriginalURL = "";
                var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
                if (statusCodeReExecuteFeature != null)
                    OriginalURL = statusCodeReExecuteFeature.OriginalPathBase
                                    + statusCodeReExecuteFeature.OriginalPath
                                    + statusCodeReExecuteFeature.OriginalQueryString;
                StatusCodeModel viewModel = new StatusCodeModel()
                {
                    RequestId = requestID,
                    OriginalUrl = OriginalURL,
                    ErrorStatusCode = statusCode.ToString()
                };
                logMsg = $"Request ID: {requestID}; OriginalURL = {OriginalURL}; " +
                                $"StatusCode = {statusCode.ToString()}";
                _logger.LogError(logMsg);

                return View("StatusCode", viewModel);
            }
            // check for specific errors, take special actions if necessary & log all exceptions
            var error = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (error is ExceptionHandlerFeature ex)
            {
                if (ex.Error is SqlException)
                    _logger.LogError($"***** SqlException for IP address: 						{Request.HttpContext.Connection.RemoteIpAddress}");
            }

            // general error logging and error page display
            ErrorViewModel errorVM = new ErrorViewModel { RequestId = requestID, RequestDescription = error.Path, RequestError = error.Error.GetType().ToString() };
            logMsg = $"Request ID: {errorVM.RequestId}; RequestDescription = {errorVM.RequestDescription}; " +
                     $"Request Error: {errorVM.RequestError}";
            _logger.LogError(logMsg);

            return View(errorVM);

        }
    }
}
