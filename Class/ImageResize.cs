using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace HEA.ePTW.Class
{
    public class ImageResize
    {
        public static byte[] ReduceImageSize(byte[] imageBytes, int jpegQuality)
        {
            var inputStream = new MemoryStream(imageBytes);
            var image = System.Drawing.Image.FromStream(inputStream);
            var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (int)jpegQuality);
            var outputStream = new MemoryStream();
            image.Save(outputStream, jpegEncoder, encoderParameters);
            return outputStream.ToArray();
        }
    }
}