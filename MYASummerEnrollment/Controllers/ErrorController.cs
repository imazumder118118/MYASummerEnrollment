using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using MYASummerEnrollment.Models;
using System.Net.Mail;
using Microsoft.Extensions.Options;


namespace MYASummerEnrollment.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IOptions<ApplicationConfig> _config;
        public ErrorController(IOptions<ApplicationConfig> config)
        {
            _config = config;
        }
        [Route("Error/{statusCode}")]
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    var stringBuilder = new StringBuilder();
                    ErrorViewModel errormodel = new ErrorViewModel();
                    errormodel.ExceptionMessage = "Sorry, the resource you requested could not be found ";
                    errormodel.ExceptionPath = statusCodeResult.OriginalPath;
                    errormodel.ErrorSource = statusCodeResult.OriginalQueryString;
                    EmailException(errormodel, stringBuilder);
                    break;
            }
            return View("Error");
        }
        [Route("Error")]

        public IActionResult Error()
        {
            var stringBuilder = new StringBuilder();
            ErrorViewModel errormodel = new ErrorViewModel();
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            errormodel.ExceptionPath = exceptionDetails.Path;
            errormodel.ExceptionMessage = exceptionDetails.Error.Message;
            errormodel.StackTrace = exceptionDetails.Error.StackTrace;
            errormodel.ErrorSource = exceptionDetails.Error.Source;
            //errormodel.InnerException = exceptionDetails.Error.InnerException.ToString();
            EmailException(errormodel, stringBuilder);

            return View("Error");
        }


        public void EmailException(ErrorViewModel exception, StringBuilder stringBuilder)
        {
            BuildExceptionText(stringBuilder, "<h1>Error </h1>", exception);

            //_emailSender.SendMailAsync(_smtpDetails.Value, new List<string> { _configuration["ErrorDeliveryEmailAddress"] },
            //    "System Error", stringBuilder.ToString()).ConfigureAwait(false);
            SmtpClient smtpClient = new SmtpClient(_config.Value.SMTPExchangeEmailServer, 25);
            smtpClient.Credentials = new System.Net.NetworkCredential("", "");
            //smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;


            MailMessage email = new MailMessage("MayorsYouthAcademy@richmondgov.com", "indranil.mazumder@richmondgov.com", "System Error ", stringBuilder.ToString());
            email.IsBodyHtml = true;

            smtpClient.Send(email);


        }

        public  StringBuilder BuildExceptionText(StringBuilder stringBuilder, string title, ErrorViewModel exception)
        {
            stringBuilder.Append(title).Append("<h2>").Append(exception.ExceptionMessage).Append("</h2><br/>").Append(exception.ErrorSource ?? "").Append("<hr/>");
            if (exception.StackTrace != null)
            {
                stringBuilder.Append("<h3>Stack trace: </h3><br/>").Append(exception.StackTrace.Replace(Environment.NewLine, "<br/>"));
            }
            if (exception.InnerException != null)
            {
                stringBuilder.Append("<h3>Inner Exception: </h3><br/>").Append(exception.InnerException.Replace(Environment.NewLine, "<br/>"));
            }

            //if (exception.StackTrace != null)
            //{
            //    BuildExceptionText(stringBuilder, "<h2>Inner exception </h2>", exception.StackTrace);
            //}

            return stringBuilder;

        }
    }
}