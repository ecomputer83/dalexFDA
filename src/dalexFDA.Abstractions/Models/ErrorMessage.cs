using System;
using System.Collections.Generic;

namespace dalexFDA.Abstractions
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public ModelState ModelState { get; set; }
    }

    public class ModelState
    {
        public List<string> array { get; set; }
    }
}
