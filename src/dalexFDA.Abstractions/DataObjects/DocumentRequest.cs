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

    public class QueryRequest
    {
        public string Client_No { get; set; }
        public string Relationship_Manager { get; set; }
        public string Message_Type { get; set; }
        public string Client_Name { get; set; }
        public string Email_Address { get; set; }
        public string Message_Detail { get; set; }
    }
}
