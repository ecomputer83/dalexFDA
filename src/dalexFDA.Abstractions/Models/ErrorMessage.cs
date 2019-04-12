using System;
using System.Collections.Generic;

namespace dalexFDA.Abstractions
{
    public class ErrorMessage
    {
        public Message error { get; set; }

        public string Message { get; set; }
        public ModelState ModelState { get; set; }
    }

    public class ModelState
    {
        public string[] error { get; set; }
    }

    public class Message
    {
        public string error_description { get; set; }
    }
}
