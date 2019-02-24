using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class Media
    {
        public string Path
        {
            get; set;
        }
        public Stream stream { get; set; }

        public Byte[] Data { get; set; }
    }
}
