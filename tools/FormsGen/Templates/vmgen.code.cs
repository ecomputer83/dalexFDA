using System;
using System.IO;

namespace FormsGen
{
    public partial class vmgen
    {
        public Page Page { get; set; }
        public Project Project { get; set; }
        public string ViewModelFilePath { get; set; }

        public bool FileContainsText(string fileName, string keyword)
        {
            bool retVal = false;

            if(File.Exists(fileName))
            {
                retVal = File.ReadAllText(fileName).Contains(keyword);    
            }
            return retVal;

        }
    }
}
