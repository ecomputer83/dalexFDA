using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Abstractions
{
    public class DocumentRequest
    {
        public byte[] File { get; set; }
        public int FileType { get; set; }
    }
}
