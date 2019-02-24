using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace dalexFDA.Abstractions
{
    public interface IImageResizer
    {
        Task<byte[]> ResizeImage(byte[] imageData, float width, float height);
        string FilePath { get; set; }
    }
}
