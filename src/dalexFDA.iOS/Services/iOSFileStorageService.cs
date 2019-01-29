using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dalexFDA.Abstractions;

namespace dalexFDA.iOS
{
    public class iOSFileStorageService : IFileStorageService
    {


        public Task<byte[]> ReadAsBytesAsync(string filename)
        {
            var data = File.ReadAllBytes(filename);

            if (data != null)
                data = CleanByteOrderMark(data);

            return Task.FromResult(data);
        }

        public async Task<string> ReadAsStringAsync(string filename)
        {
            var data = await ReadAsBytesAsync(filename);

            if (data == null)
                return string.Empty;

            return System.Text.Encoding.UTF8.GetString(data);
        }

        public byte[] ReadAsBytes(string filename)
        {
            var data = File.ReadAllBytes(filename);

            if (data != null)
                data = CleanByteOrderMark(data);

            return data;
        }

        public string ReadAsString(string filename)
        {
            var data = ReadAsBytes(filename);

            if (data == null)
                return string.Empty;

            return System.Text.Encoding.UTF8.GetString(data);
        }

        public byte[] CleanByteOrderMark(byte[] bytes)
        {
            var bom = new byte[] { 0xEF, 0xBB, 0xBF };
            var empty = Enumerable.Empty<byte>();
            if (bytes.Take(3).SequenceEqual(bom))
                return bytes.Skip(3).ToArray();

            return bytes;
        }
    }
}
