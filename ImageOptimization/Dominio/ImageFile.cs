namespace ImageOptimization.Dominio;

public class ImageFile
{
    public string Name { get; set; } = "";

    public IFormFile? Image { get; set; }
}
