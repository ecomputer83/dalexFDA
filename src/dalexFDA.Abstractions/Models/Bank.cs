using System;
namespace dalexFDA.Abstractions
{
    public class Bank
    {
        public Bank()
        {
        }

        public int Code { get; set; }
        public string Name { get; set; }
    }

    public class Mode
    {
        public Mode()
        {
        }

        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class ReqType
    {
        public ReqType()
        {
        }

        public int Code { get; set; }
        public string Name { get; set; }
    }
}
