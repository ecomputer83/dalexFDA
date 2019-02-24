using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace dalexFDA.Data.WebServices
{
    public class DocumentRequest
    {
        [AttachmentName("photo")]
        public byte[] File { get; set; }
        public int FileType { get; set; }
    }
}
