using System;

namespace MYASummerEnrollment.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ExceptionPath { get; set;}

        public string ExceptionMessage { get; set; }


        public string StackTrace { get; set; }
        
        public string ErrorSource { get; set; }

        public string InnerException { get; set; }



    }
}
