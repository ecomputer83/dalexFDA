using System;
using System.IO;
using Android.App;
using Android.Content;
using dalexFDA.Abstractions;

namespace dalexFDA.Droid
{
    public class AndroidFileStorageService : IFileStorageService
    {
        private Context _context = Application.Context;

        public string ReadAsString(string fileName)
        {
            try
            {
                using (var asset = _context.Assets.Open(fileName))
                {
                    using (var streamReader = new StreamReader(asset))
                    {
                        var retVal = streamReader.ReadToEnd();
                        return retVal;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
