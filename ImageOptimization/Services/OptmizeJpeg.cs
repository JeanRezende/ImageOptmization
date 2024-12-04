using MozJpegSharp;
using ImageOptimization.Dominio;

namespace ImageOptimization.Services
{
    public class OptmizeJpeg
    {
        private static async Task WriteImage(string imageName, byte[] image)
        {
            try
            {
                await File.WriteAllBytesAsync(@$"ProcessedImages/{imageName}", image);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " falhou ao escrever");
            }
        }

        private static byte[] Convert(IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        private static byte[] GetOptmizedImageAsync(byte[] image)
        {
            return MozJpeg.Recompress(image.AsSpan(), quality: 80);
        }
        public async Task ProcessImage(ImageFile imageFile)
        {
            byte[] convertedImage = Convert(imageFile.Image);
            byte[] optmizedImage = GetOptmizedImageAsync(convertedImage);

            await WriteImage(imageFile.Name, optmizedImage);
        }
    }
}