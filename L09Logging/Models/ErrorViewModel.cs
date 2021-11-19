using System;

namespace L09Logging.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string RequestDescription { get; set; }
        public string RequestError { get; set; }
    }
}
