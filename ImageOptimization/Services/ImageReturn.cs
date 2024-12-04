namespace ImageOptimization.Services;

public class ImageReturn
{
    public MemoryStream ProcessReturn()
    {
        string lastImage = LastImage();
        return GetImageStream(lastImage);
    }
    public static MemoryStream GetImageStream(string pathImage)
    {

        using FileStream imageStr = File.Open(pathImage, FileMode.Open);
        using MemoryStream memStr = new();
        imageStr.CopyTo(memStr);
        return memStr;
    }
    /*
            private async Task<Image> LoadImage(string pathImage) {
                if( pathImage != "No file found") {
                    Image image = await Image.LoadAsync(pathImage);
                    return image;
                }
                throw new Exception("No file found");
            }
    */
    readonly string basePath = @"C:\Users\jean.rezende\aulas\Portifolio\ImageOptimization\ProcessedImages";
    /*
            private string PathLastImage(string imageName) {
                string pathLastImage = $@"\{imageName}";
                    return pathLastImage;

                throw new Exception("No file found");
            }
    */
    private string LastImage()
    {
        string[] files = Directory.GetFiles(basePath);
        if (files.Length != 0)
        {
            DateTime creation = Directory.GetCreationTime(files[0]);
            DateTime fileDateTime;
            string returnFile = files[0];

            foreach (string file in files)
            {
                fileDateTime = Directory.GetCreationTime(file);
                if (creation <= fileDateTime)
                {
                    creation = fileDateTime;
                    returnFile = file;
                }
            }
            return returnFile;
        }
        throw new Exception("No file found");
    }
}
